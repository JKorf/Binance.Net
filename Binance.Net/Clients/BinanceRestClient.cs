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
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BinanceRestClient(Action<BinanceRestOptions>? optionsDelegate = null) : base(null, "Binance")
        {
            var options = new BinanceRestOptions();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            GeneralApi = AddApiClient(new BinanceRestClientGeneralApi(_logger, null, this, options));
            SpotApi = AddApiClient(new BinanceRestClientSpotApi(_logger, null, options));
            UsdFuturesApi = AddApiClient(new BinanceRestClientUsdFuturesApi(_logger, null, options));
            CoinFuturesApi = AddApiClient(new BinanceRestClientCoinFuturesApi(_logger, null, options));
        }

        /// <summary>
        /// Create a new instance of the BinanceRestClient using provided options
        /// </summary>
        /// <param name="optionConfig">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public BinanceRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<BinanceRestOptions>? optionConfig = null) : base(loggerFactory, "Binance")
        {
            var options = optionConfig?.Value ?? new BinanceRestOptions();
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
            var options = BinanceRestOptions.Default.Copy();
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
