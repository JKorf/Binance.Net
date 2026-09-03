using CryptoExchange.Net.Interfaces.Clients;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD futures streams
    /// </summary>
    public interface IBinanceSocketClientUsdFuturesApi : ISocketApiClient<BinanceCredentials>, IDisposable
    {
        /// <summary>
        /// [V1] Get the shared socket subscription client. For new implementations prefer using <see cref="SharedApi"/>
        /// </summary>
        IBinanceSocketClientUsdFuturesApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        IBinanceSocketClientUsdFuturesSharedApi SharedApi { get; }

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
