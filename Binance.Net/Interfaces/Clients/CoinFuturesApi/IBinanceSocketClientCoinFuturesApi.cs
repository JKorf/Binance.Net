namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Binance Coin futures streams
    /// </summary>
    public interface IBinanceSocketClientCoinFuturesApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBinanceSocketClientCoinFuturesApiShared SharedClient { get; }

        /// <summary>
        /// Account streams and queries
        /// </summary>
        IBinanceSocketClientCoinFuturesApiAccount Account { get; }
        /// <summary>
        /// Exchange data streams and queries
        /// </summary>
        IBinanceSocketClientCoinFuturesApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Trading data and queries
        /// </summary>
        IBinanceSocketClientCoinFuturesApiTrading Trading { get; }
    }
}
