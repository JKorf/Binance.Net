using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using Binance.Net.SymbolOrderBooks;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Configuration;
using System.Net;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the Binance.Net services:<br />
        /// <see cref="IBinanceRestClient">IBinanceRestClient</see>: Client for accessing the Binance REST API<br />
        /// <see cref="IBinanceSocketClient">IBinanceSocketClient</see>: Client for accessing the Binance Socket API<br />
        /// <see cref="IBinanceOrderBookFactory">IBinanceOrderBookFactory</see>: Factory for creating locally synced order books
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultRestOptionsDelegate">Set default options for the rest client</param>
        /// <param name="defaultSocketOptionsDelegate">Set default options for the socket client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IBinanceSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddBinance(
            this IServiceCollection services,
            Action<BinanceRestOptions>? defaultRestOptionsDelegate = null,
            Action<BinanceSocketOptions>? defaultSocketOptionsDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            if (defaultRestOptionsDelegate != null)
                services.Configure(defaultRestOptionsDelegate);
            if (defaultSocketOptionsDelegate != null)
                services.Configure(defaultSocketOptionsDelegate);

            AddServices(services, socketClientLifeTime);
            return services;
        }

        /// <summary>
        /// Add the Binance.Net services:<br />
        /// <see cref="IBinanceRestClient">IBinanceRestClient</see>: Client for accessing the Binance REST API<br />
        /// <see cref="IBinanceSocketClient">IBinanceSocketClient</see>: Client for accessing the Binance Socket API<br />
        /// <see cref="IBinanceOrderBookFactory">IBinanceOrderBookFactory</see>: Factory for creating locally synced order books
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The IConfiguration instance</param>
        /// <param name="socketClientLifeTime">The lifetime of the IBinanceSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddBinance(
            this IServiceCollection services,
            IConfiguration configuration,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.Configure<BinanceRestOptions>(configuration.GetSection("Binance").GetSection("Rest"));
            services.Configure<BinanceSocketOptions>(configuration.GetSection("Binance").GetSection("Socket"));
            AddServices(services, socketClientLifeTime);
            return services;
        }

        private static void AddServices(IServiceCollection services, ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IBinanceRestClient, BinanceRestClient>((httpClient, serviceProvider) =>
            {
                var restOptions = serviceProvider.GetRequiredService<IOptions<BinanceRestOptions>>();
                httpClient.Timeout = restOptions.Value.RequestTimeout;
                return new BinanceRestClient(httpClient, serviceProvider.GetRequiredService<ILoggerFactory>(), restOptions);
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var restOptions = serviceProvider.GetRequiredService<IOptions<BinanceRestOptions>>();
                var handler = new HttpClientHandler();
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (restOptions.Value.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{restOptions.Value.Proxy.Host}:{restOptions.Value.Proxy.Port}"),
                        Credentials = restOptions.Value.Proxy.Password == null ? null : new NetworkCredential(restOptions.Value.Proxy.Login, restOptions.Value.Proxy.Password)
                    };
                }
                return handler;
            });

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<IBinanceOrderBookFactory, BinanceOrderBookFactory>();
            services.AddTransient(x => x.GetRequiredService<IBinanceRestClient>().SpotApi.CommonSpotClient);
            services.AddTransient(x => x.GetRequiredService<IBinanceRestClient>().UsdFuturesApi.CommonFuturesClient);
            services.AddTransient(x => x.GetRequiredService<IBinanceRestClient>().CoinFuturesApi.CommonFuturesClient);
            services.Add(new ServiceDescriptor(typeof(IBinanceSocketClient), (serviceProvider) =>
            {
                var socketOptions = serviceProvider.GetRequiredService<IOptions<BinanceSocketOptions>>();
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                return new BinanceSocketClient(socketOptions, loggerFactory);
            }, socketClientLifeTime ?? ServiceLifetime.Singleton));
        }
    }
}
