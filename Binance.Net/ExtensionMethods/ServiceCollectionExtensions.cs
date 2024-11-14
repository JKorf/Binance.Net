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
using System.Net;
using System;

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
            configuration.Bind(options);

            services.Configure<BinanceRestOptions>(x =>
            {
                options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? x.Environment;
                options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials ?? x.ApiCredentials;
                options.Rest.Set(x);
            });
            services.PostConfigure<BinanceRestOptions>(x =>
            {
                x.Environment = x.Environment.Name switch
                {
                    "live" => BinanceEnvironment.Live,
                    "testnet" => BinanceEnvironment.Testnet,
                    "us" => BinanceEnvironment.Us,
                    null => BinanceEnvironment.Live,
                    _ => x.Environment
                };
            });
            services.Configure<BinanceSocketOptions>(x =>
            {
                options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? x.Environment;
                options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials ?? x.ApiCredentials;
                options.Socket.Set(x);
            });
            services.PostConfigure<BinanceSocketOptions>(x =>
            {
                x.Environment = x.Environment?.Name switch
                {
                    "live" => BinanceEnvironment.Live,
                    "testnet" => BinanceEnvironment.Testnet,
                    "us" => BinanceEnvironment.Us,
                    null => BinanceEnvironment.Live,
                    _ => x.Environment
                };
            });

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
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restOptions = services.AddOptions<BinanceRestOptions>().Configure(x => {
                options.Rest.Environment = options.Environment ?? x.Environment;
                options.Rest.ApiCredentials = options.ApiCredentials ?? x.ApiCredentials;
                options.Rest.Set(x);
            });
            var socketOptions = services.AddOptions<BinanceSocketOptions>().Configure(x => {
                options.Socket.Environment = options.Environment ?? x.Environment;
                options.Socket.ApiCredentials = options.ApiCredentials ?? x.ApiCredentials;
                options.Socket.Set(x);
            });
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
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var handler = new HttpClientHandler();
                try
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                catch (PlatformNotSupportedException)
                { }

                var options = serviceProvider.GetRequiredService<IOptions<BinanceRestOptions>>().Value;
                if (options.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{options.Proxy.Host}:{options.Proxy.Port}"),
                        Credentials = options.Proxy.Password == null ? null : new NetworkCredential(options.Proxy.Login, options.Proxy.Password)
                    };
                }
                return handler;
            });
            services.Add(new ServiceDescriptor(typeof(IBinanceSocketClient), x => { return new BinanceSocketClient(x.GetRequiredService<IOptions<BinanceSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<IBinanceOrderBookFactory, BinanceOrderBookFactory>();
            services.AddTransient<IBinanceTrackerFactory, BinanceTrackerFactory>();
            services.AddTransient(x => x.GetRequiredService<IBinanceRestClient>().SpotApi.CommonSpotClient);
            services.AddTransient(x => x.GetRequiredService<IBinanceRestClient>().UsdFuturesApi.CommonFuturesClient);
            services.AddTransient(x => x.GetRequiredService<IBinanceRestClient>().CoinFuturesApi.CommonFuturesClient);

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
