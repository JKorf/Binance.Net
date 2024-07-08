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
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Binance.Net.Clients
{
    /// <inheritdoc cref="IBinanceRestClient" />
    public class BinanceRestClient : BaseRestClient, IBinanceRestClient
    {
        /// <summary>
        /// Default options
        /// </summary>
        public static BinanceRestOptions DefaultOptions { get; private set; } = new BinanceRestOptions()
        {
            Environment = BinanceEnvironment.Live,
            AutoTimestamp = true
        };

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
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="httpClient">Http client for this client</param>
        public BinanceRestClient(Action<BinanceRestOptions>? optionsDelegate = null, HttpClient? httpClient = null) : base(null, "Binance")
        {
            var options = DefaultOptions.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            GeneralApi = AddApiClient(new BinanceRestClientGeneralApi(_logger, httpClient, this, options));
            SpotApi = AddApiClient(new BinanceRestClientSpotApi(_logger, httpClient, options));
            UsdFuturesApi = AddApiClient(new BinanceRestClientUsdFuturesApi(_logger, httpClient, options));
            CoinFuturesApi = AddApiClient(new BinanceRestClientCoinFuturesApi(_logger, httpClient, options));
        }

        /// <summary>
        /// Create a new instance of the BinanceRestClient using provided options
        /// </summary>
        /// <param name="optionConfig">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        [ActivatorUtilitiesConstructor]
        public BinanceRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<BinanceRestOptions>? optionConfig = null) : base(loggerFactory, "Binance")
        {
            var options = optionConfig?.Value ?? DefaultOptions.Copy(); // Environment not set, how does that work form config?
            Initialize(options);

            GeneralApi = AddApiClient(new BinanceRestClientGeneralApi(_logger, httpClient, this, options));
            SpotApi = AddApiClient(new BinanceRestClientSpotApi(_logger, httpClient, options));
            UsdFuturesApi = AddApiClient(new BinanceRestClientUsdFuturesApi(_logger, httpClient, options));
            CoinFuturesApi = AddApiClient(new BinanceRestClientCoinFuturesApi(_logger, httpClient, options));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BinanceRestOptions> optionsDelegate)
        {
            optionsDelegate(DefaultOptions);
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
