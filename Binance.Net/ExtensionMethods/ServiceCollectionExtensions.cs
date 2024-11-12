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
            services.Configure<BinanceRestOptions>(configuration.GetSection("Rest"));
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
            services.Configure<BinanceSocketOptions>(configuration.GetSection("Socket"));
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

            var socketStr = configuration["SocketClientLifeTime"];
            var lifeTimeSocket = socketStr == null ? (ServiceLifetime?)null : (ServiceLifetime)Enum.Parse(typeof(ServiceLifetime), socketStr);
            return AddBinanceCore(services, lifeTimeSocket);
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
            if (options.RestOptions == null || options.SocketOptions == null)
                throw new ArgumentException("Options null");

            var restOptions = services.AddOptions<BinanceRestOptions>().Configure(x => { options.RestOptions.Set(x); });
            var socketOptions = services.AddOptions<BinanceSocketOptions>().Configure(x => { options.SocketOptions.Set(x); });
            return AddBinanceCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddBinanceCore(
            IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IBinanceRestClient, BinanceRestClient>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BinanceRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
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

            services.Add(new ServiceDescriptor(typeof(IBinanceSocketClient), typeof(BinanceSocketClient), socketClientLifeTime ?? ServiceLifetime.Singleton));

            return services;
        }
    }
}
