using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Clients.GeneralApi;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Clients.UsdFuturesApi;
using Binance.Net.Clients.CoinFuturesApi;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Binance.Net.Clients
{
    /// <inheritdoc cref="IBinanceRestClient" />
    public class BinanceRestClient : BaseRestClient, IBinanceRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public IBinanceRestClientGeneralApi GeneralApi { get; }
        /// <inheritdoc />
        public IBinanceRestClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IBinanceRestClientUsdFuturesApi UsdFuturesApi { get; }
        /// <inheritdoc />
        public IBinanceRestClientCoinFuturesApi CoinFuturesApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the BinanceRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        public BinanceRestClient(BinanceRestOptions? options = null) 
            : this(null, null, Options.Create(options ?? new BinanceRestOptions()))
        {
        }

        /// <summary>
        /// Create a new instance of the BinanceRestClient
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options"></param>
        public BinanceRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, BinanceRestOptions? options) : this(httpClient, loggerFactory, Options.Create(options ?? new BinanceRestOptions()))
        {
        }

        /// <summary>
        /// Create a new instance of the BinanceRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        [ActivatorUtilitiesConstructor]
        public BinanceRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<BinanceRestOptions> options) : base(loggerFactory, "Binance")
        {
            Initialize(options.Value);

            GeneralApi = AddApiClient(new BinanceRestClientGeneralApi(_logger, httpClient, this, options.Value));
            SpotApi = AddApiClient(new BinanceRestClientSpotApi(_logger, httpClient, options.Value));
            UsdFuturesApi = AddApiClient(new BinanceRestClientUsdFuturesApi(_logger, httpClient, options.Value));
            CoinFuturesApi = AddApiClient(new BinanceRestClientCoinFuturesApi(_logger, httpClient, options.Value));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BinanceRestOptions> optionsDelegate)
        {
            var options = new BinanceRestOptions();
            optionsDelegate(options);
            BinanceRestOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            GeneralApi.SetApiCredentials(credentials);
            SpotApi.SetApiCredentials(credentials);
            UsdFuturesApi.SetApiCredentials(credentials);
            CoinFuturesApi.SetApiCredentials(credentials);
        }
    }
}
