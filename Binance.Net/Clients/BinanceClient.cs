using Binance.Net.Objects;
using CryptoExchange.Net;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Clients.GeneralApi;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Clients.UsdFuturesApi;
using Binance.Net.Clients.CoinFuturesApi;

namespace Binance.Net.Clients
{
    /// <inheritdoc cref="IBinanceClient" />
    public class BinanceClient : BaseRestClient, IBinanceClient
    {
        #region Api clients

        /// <inheritdoc />
        public IBinanceClientGeneralApi GeneralApi { get; }
        /// <inheritdoc />
        public IBinanceClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IBinanceClientUsdFuturesApi UsdFuturesApi { get; }
        /// <inheritdoc />
        public IBinanceClientCoinFuturesApi CoinFuturesApi { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceClient using the default options
        /// </summary>
        public BinanceClient() : this(BinanceClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceClient(BinanceClientOptions options) : base("Binance", options)
        {
            GeneralApi = AddApiClient(new BinanceClientGeneralApi(log, this, options));
            SpotApi = AddApiClient(new BinanceClientSpotApi(log, options));
            UsdFuturesApi = AddApiClient(new BinanceClientUsdFuturesApi(log, options));
            CoinFuturesApi = AddApiClient(new BinanceClientCoinFuturesApi(log, options));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(BinanceClientOptions options)
        {
            BinanceClientOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(BinanceApiCredentials credentials)
        {
            GeneralApi.SetApiCredentials(credentials);
            SpotApi.SetApiCredentials(credentials);
            UsdFuturesApi.SetApiCredentials(credentials);
            CoinFuturesApi.SetApiCredentials(credentials);
        }
    }
}
