using Binance.Net.Clients.CoinFuturesApi;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Clients.UsdFuturesApi;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects;
using CryptoExchange.Net;

namespace Binance.Net.Clients
{
    /// <inheritdoc cref="IBinanceSocketClient" />
    public class BinanceSocketClient : BaseSocketClient, IBinanceSocketClient
    {
        #region fields
        #endregion

        #region Api clients

        /// <inheritdoc />
        public IBinanceSocketClientSpotStreams SpotStreams { get; set; }
        /// <inheritdoc />
        public IBinanceSocketClientUsdFuturesStreams UsdFuturesStreams { get; set; }
        /// <inheritdoc />
        public IBinanceSocketClientCoinFuturesStreams CoinFuturesStreams { get; set; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClientSpot with default options
        /// </summary>
        public BinanceSocketClient() : this(BinanceSocketClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceSocketClientSpot using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceSocketClient(BinanceSocketClientOptions options) : base("Binance", options)
        {
            SpotStreams = AddApiClient(new BinanceSocketClientSpotStreams(log, options));
            UsdFuturesStreams = AddApiClient(new BinanceSocketClientUsdFuturesStreams(log, options));
            CoinFuturesStreams = AddApiClient(new BinanceSocketClientCoinFuturesStreams(log, options));
        }
        #endregion 

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(BinanceSocketClientOptions options)
        {
            BinanceSocketClientOptions.Default = options;
        }
    }
}
