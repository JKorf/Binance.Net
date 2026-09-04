using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;

namespace Binance.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Binance
    /// </summary>
    public interface IBinanceSharedApiClient
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IBinanceRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// USD futures REST shared API implementations
        /// </summary>
        IBinanceRestClientUsdFuturesSharedApi UsdFuturesRest { get; }

        /// <summary>
        /// Coin futures REST shared API implementations
        /// </summary>
        IBinanceRestClientCoinFuturesSharedApi CoinFuturesRest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IBinanceSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// USD futures WebSocket shared API implementations
        /// </summary>
        IBinanceSocketClientUsdFuturesSharedApi UsdFuturesSocket { get; }

        /// <summary>
        /// Coin futures WebSocket shared API implementations
        /// </summary>
        IBinanceSocketClientCoinFuturesSharedApi CoinFuturesSocket { get; }
    }
}
