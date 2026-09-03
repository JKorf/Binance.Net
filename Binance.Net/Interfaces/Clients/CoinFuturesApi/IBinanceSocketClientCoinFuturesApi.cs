using CryptoExchange.Net.Interfaces.Clients;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Binance Coin futures streams
    /// </summary>
    public interface IBinanceSocketClientCoinFuturesApi : ISocketApiClient<BinanceCredentials>, IDisposable
    {
        /// <summary>
        /// [V1] Get the shared socket subscription client. For new implementations prefer using <see cref="SharedApi"/>
        /// </summary>
        IBinanceSocketClientCoinFuturesApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        IBinanceSocketClientCoinFuturesSharedApi SharedApi { get; }

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
