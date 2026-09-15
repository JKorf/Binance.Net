using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using Binance.Net.SymbolOrderBooks;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the IBinanceRestClient and IBinanceSocketClient. Configures the services based on the provided configuration.<br />
        /// See <see href="https://github.com/JKorf/Binance.Net/blob/master/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddBinance(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = BinanceOptions.Create(configuration);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

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
            var options = BinanceOptions.Create(optionsDelegate);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

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
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IBinanceSocketClient), x => { return new BinanceSocketClient(x.GetRequiredService<IOptions<BinanceSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IBinanceOrderBookFactory, BinanceOrderBookFactory>();
            services.AddTransient<IBinanceTrackerFactory, BinanceTrackerFactory>();
            services.AddTransient<ITrackerFactory, BinanceTrackerFactory>();
            services.AddSingleton<IBinanceUserClientProvider, BinanceUserClientProvider>(x =>
            new BinanceUserClientProvider(
                x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IBinanceRestClient).Name),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<BinanceRestOptions>>(),
                x.GetRequiredService<IOptions<BinanceSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBinanceRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBinanceSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBinanceRestClient>().UsdFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBinanceSocketClient>().UsdFuturesApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBinanceRestClient>().CoinFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBinanceSocketClient>().CoinFuturesApi.SharedClient);

            services.RegisterSharedApiClient<
                IBinanceSharedApiClient,
                BinanceSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.SpotRest)
                    .Add(client => client.SpotSocket)
                    .Add(client => client.UsdFuturesRest)
                    .Add(client => client.UsdFuturesSocket)
                    .Add(client => client.CoinFuturesRest)
                    .Add(client => client.CoinFuturesSocket));

            return services;
        }
    }
}
