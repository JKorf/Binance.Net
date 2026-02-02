using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using Binance.Net.SymbolOrderBooks;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using CryptoExchange.Net.Interfaces.Clients;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the IBinanceRestClient and IBinanceSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddBinance(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new BinanceOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            configuration.Bind(options);

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? BinanceEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? BinanceEnvironment.Live.Name;
            options.Rest.AllowAppendingClientOrderId = options.Rest.AllowAppendingClientOrderId || options.AllowAppendingClientOrderId;
            options.Rest.Environment = BinanceEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.AllowAppendingClientOrderId = options.Socket.AllowAppendingClientOrderId || options.AllowAppendingClientOrderId;
            options.Socket.Environment = BinanceEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;


            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddBinanceCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IBinanceRestClient and IBinanceSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the Binance services</param>
        /// <returns></returns>
        public static IServiceCollection AddBinance(
            this IServiceCollection services,
            Action<BinanceOptions>? optionsDelegate = null)
        {
            var options = new BinanceOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? BinanceEnvironment.Live;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Rest.AllowAppendingClientOrderId = options.Rest.AllowAppendingClientOrderId || options.AllowAppendingClientOrderId;
            options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? BinanceEnvironment.Live;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;
            options.Socket.AllowAppendingClientOrderId = options.Socket.AllowAppendingClientOrderId || options.AllowAppendingClientOrderId;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddBinanceCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddBinanceCore(
            IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IBinanceRestClient, BinanceRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BinanceRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new BinanceRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<BinanceRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BinanceRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options.Proxy, options.HttpKeepAliveInterval);
            });
            services.Add(new ServiceDescriptor(typeof(IBinanceSocketClient), x => { return new BinanceSocketClient(x.GetRequiredService<IOptions<BinanceSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<IBinanceOrderBookFactory, BinanceOrderBookFactory>();
            services.AddTransient<IBinanceTrackerFactory, BinanceTrackerFactory>();
            services.AddTransient<ITrackerFactory, BinanceTrackerFactory>();
            services.AddSingleton<IBinanceUserClientProvider, BinanceUserClientProvider>(x =>
            new BinanceUserClientProvider(
                x.GetRequiredService<HttpClient>(),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<BinanceRestOptions>>(),
                x.GetRequiredService<IOptions<BinanceSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBinanceRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBinanceSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBinanceRestClient>().UsdFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBinanceSocketClient>().UsdFuturesApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBinanceRestClient>().CoinFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBinanceSocketClient>().CoinFuturesApi.SharedClient);

            return services;
        }
    }
}
