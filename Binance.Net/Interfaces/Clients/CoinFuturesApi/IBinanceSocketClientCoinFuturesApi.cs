using CryptoExchange.Net.Interfaces.Clients;

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
        /// <see cref="IBinanceSocketClientCoinFuturesApiAccount"/>
        IBinanceSocketClientCoinFuturesApiAccount Account { get; }
        /// <summary>
        /// Exchange data streams and queries
        /// </summary>
        /// <see cref="IBinanceSocketClientCoinFuturesApiExchangeData"/>
        IBinanceSocketClientCoinFuturesApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Trading data and queries
        /// </summary>
        /// <see cref="IBinanceSocketClientCoinFuturesApiTrading"/>
        IBinanceSocketClientCoinFuturesApiTrading Trading { get; }
    }
}
