using CryptoExchange.Net.SharedApis.Interfaces;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API socket subscriptions and requests
    /// </summary>
    public interface IBinanceSocketClientSpotApi : ISocketApiClient
    {
        /// <summary>
        /// Account streams and queries
        /// </summary>
        IBinanceSocketClientSpotApiAccount Account { get; }
        /// <summary>
        /// Exchange data streams and queries
        /// </summary>
        IBinanceSocketClientSpotApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Trading data and queries
        /// </summary>
        IBinanceSocketClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the shared socket subscription client
        /// </summary>
        IBinanceSocketClientSpotApiShared SharedClient { get; }
    }
}