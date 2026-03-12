using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace Binance.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Binance websocket API
    /// </summary>
    public interface IBinanceSocketClient : ISocketClient<BinanceCredentials>
    {
        /// <summary>
        /// Coin futures streams
        /// </summary>
        /// <see cref="IBinanceSocketClientCoinFuturesApi"/>
        IBinanceSocketClientCoinFuturesApi CoinFuturesApi { get; }
        /// <summary>
        /// Spot streams and requests
        /// </summary>
        /// <see cref="IBinanceSocketClientSpotApi"/>
        IBinanceSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usd futures streams
        /// </summary>
        /// <see cref="IBinanceSocketClientUsdFuturesApi"/>
        IBinanceSocketClientUsdFuturesApi UsdFuturesApi { get; }
    }
}
