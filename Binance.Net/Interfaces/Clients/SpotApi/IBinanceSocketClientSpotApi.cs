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
        /// <see cref="IBinanceSocketClientSpotApiAccount"/>
        IBinanceSocketClientSpotApiAccount Account { get; }
        /// <summary>
        /// Exchange data streams and queries
        /// </summary>
        /// <see cref="IBinanceSocketClientSpotApiExchangeData"/>
        IBinanceSocketClientSpotApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Trading data and queries
        /// </summary>
        /// <see cref="IBinanceSocketClientSpotApiTrading"/>
        IBinanceSocketClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBinanceSocketClientSpotApiShared SharedClient { get; }
    }
}