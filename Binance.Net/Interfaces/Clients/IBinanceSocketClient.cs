using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Binance websocket API
    /// </summary>
    public interface IBinanceSocketClient: ISocketClient
    {
        /// <summary>
        /// Coin futures streams
        /// </summary>
        IBinanceSocketClientCoinFuturesStreams CoinFuturesStreams { get; }
        /// <summary>
        /// Spot streams
        /// </summary>
        IBinanceSocketClientSpotStreams SpotStreams { get; }
        /// <summary>
        /// Usd futures streams
        /// </summary>
        IBinanceSocketClientUsdFuturesStreams UsdFuturesStreams { get; }
    }
}
