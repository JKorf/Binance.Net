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
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="socketClientLifeTime"></param>
        /// <returns></returns>
        public static IServiceCollection AddBinance(
            this IServiceCollection services,
            IConfiguration configuration,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.Configure<BinanceRestOptions>(configuration);
            services.PostConfigure<BinanceRestOptions>(x =>
            {
                if (x.Environment == null || x.Environment.Name == BinanceEnvironment.Live.Name)
                    x.Environment = BinanceEnvironment.Live;
                else if (x.Environment.Name == BinanceEnvironment.Testnet.Name)
                    x.Environment = BinanceEnvironment.Testnet;
                else if (x.Environment.Name == BinanceEnvironment.Us.Name)
                    x.Environment = BinanceEnvironment.Us;
            });
            services.Configure<BinanceSocketOptions>(configuration);
            services.PostConfigure<BinanceSocketOptions>(x =>
            {
                if (x.Environment == null || x.Environment.Name == BinanceEnvironment.Live.Name)
                    x.Environment = BinanceEnvironment.Live;
                else if (x.Environment.Name == BinanceEnvironment.Testnet.Name)
                    x.Environment = BinanceEnvironment.Testnet;
                else if (x.Environment.Name == BinanceEnvironment.Us.Name)
                    x.Environment = BinanceEnvironment.Us;
            });
            return AddBinanceCore(services, socketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IBinanceRestClient and IBinanceSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="restOptionsDelegate">Set options for the rest client</param>
        /// <param name="socketOptionsDelegate">Set options for the socket client</param>
        /// <param name="restClientLifeTime">The lifetime of the IBinanceRestClient for the service collection. Defaults to Transient.</param>
        /// <param name="socketClientLifeTime">The lifetime of the IBinanceSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddBinance(
            this IServiceCollection services,
            Action<BinanceRestOptions>? restOptionsDelegate = null,
            Action<BinanceSocketOptions>? socketOptionsDelegate = null,
            ServiceLifetime? restClientLifeTime = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            var restOptions = services.AddOptions<BinanceRestOptions>();
            restOptions.Configure(x =>
            {
                restOptionsDelegate?.Invoke(x);
            });

            var socketOptions = services.AddOptions<BinanceSocketOptions>();
            if (socketOptionsDelegate != null)
            {
                socketOptions.Configure(socketOptionsDelegate);
                BinanceSocketClient.SetDefaultOptions(socketOptionsDelegate);
            }

            return AddBinanceCore(services, restClientLifeTime, socketClientLifeTime);
        }

        private static IServiceCollection AddBinanceCore(IServiceCollection services, ServiceLifetime? restClientLifeTime = null, ServiceLifetime? socketClientLifeTime = null)
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

            if (socketClientLifeTime == null)
                services.AddSingleton<IBinanceSocketClient, BinanceSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IBinanceSocketClient), typeof(BinanceSocketClient), socketClientLifeTime.Value));
            return services;
        }
    }
}
