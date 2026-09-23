using CryptoExchange.Net.Interfaces.Clients;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API socket subscriptions and requests
    /// </summary>
    public interface IBinanceSocketClientSpotApi : ISocketApiClient<BinanceCredentials>
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
        /// [V1] Get the shared socket subscription client. For new implementations prefer using <see cref="SharedApi"/>
        /// </summary>
        IBinanceSocketClientSpotApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        IBinanceSocketClientSpotSharedApi SharedApi { get; }
    }
}