using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using Binance.Net.SymbolOrderBooks;
using CryptoExchange.Net.Clients;
using System.Net;

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
            var restOptions = BinanceRestOptions.Default.Copy();

            if (defaultRestOptionsDelegate != null)
            {
                defaultRestOptionsDelegate(restOptions);
                BinanceRestClient.SetDefaultOptions(defaultRestOptionsDelegate);
            }

            if (defaultSocketOptionsDelegate != null)
                BinanceSocketClient.SetDefaultOptions(defaultSocketOptionsDelegate);

            services.AddHttpClient<IBinanceRestClient, BinanceRestClient>(options =>
            {
                options.Timeout = restOptions.RequestTimeout;
            }).ConfigurePrimaryHttpMessageHandler(() => {
                var handler = new HttpClientHandler();
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (restOptions.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{restOptions.Proxy.Host}:{restOptions.Proxy.Port}"),
                        Credentials = restOptions.Proxy.Password == null ? null : new NetworkCredential(restOptions.Proxy.Login, restOptions.Proxy.Password)
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
