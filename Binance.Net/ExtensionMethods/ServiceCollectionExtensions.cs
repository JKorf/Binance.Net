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

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the IBinanceClient and IBinanceSocketClient to the sevice collection so they can be injected
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
            services.AddHttpClient<IBinanceRestClient, BinanceRestClient>((serviceProvider, options) =>
            {
                var restOptions = serviceProvider.GetRequiredService<IOptions<BinanceRestOptions>>();
                options.Timeout = restOptions.Value.RequestTimeout;
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
            if (socketClientLifeTime == null)
                services.AddSingleton<IBinanceSocketClient, BinanceSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IBinanceSocketClient), typeof(BinanceSocketClient), socketClientLifeTime.Value));
        }
    }
}
