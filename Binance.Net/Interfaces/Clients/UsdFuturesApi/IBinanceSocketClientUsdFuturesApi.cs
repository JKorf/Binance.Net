namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD futures streams
    /// </summary>
    public interface IBinanceSocketClientUsdFuturesApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBinanceSocketClientUsdFuturesApiShared SharedClient { get; }

        /// <summary>
        /// Account streams and queries
        /// </summary>
        /// <see cref="IBinanceSocketClientUsdFuturesApiAccount"/>
        IBinanceSocketClientUsdFuturesApiAccount Account { get; }
        /// <summary>
        /// Exchange data streams and queries
        /// </summary>
        /// <see cref="IBinanceSocketClientUsdFuturesApiExchangeData"/>
        IBinanceSocketClientUsdFuturesApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Trading data and queries
        /// </summary>
        /// <see cref="IBinanceSocketClientUsdFuturesApiTrading"/>
        IBinanceSocketClientUsdFuturesApiTrading Trading { get; }
    }
}
