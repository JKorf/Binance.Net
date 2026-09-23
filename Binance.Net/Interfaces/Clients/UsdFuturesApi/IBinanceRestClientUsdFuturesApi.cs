using CryptoExchange.Net.Interfaces.Clients;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD futures API endpoints
    /// </summary>
    public interface IBinanceRestClientUsdFuturesApi : IRestApiClient<BinanceCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IBinanceRestClientUsdFuturesApiAccount"/>
        public IBinanceRestClientUsdFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        /// <see cref="IBinanceRestClientUsdFuturesApiExchangeData"/>
        public IBinanceRestClientUsdFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IBinanceRestClientUsdFuturesApiTrading"/>
        public IBinanceRestClientUsdFuturesApiTrading Trading { get; }

        /// <summary>
        /// [V1] Get the shared rest requests client. For new implementations prefer using <see cref="SharedApi"/>
        /// </summary>
        public IBinanceRestClientUsdFuturesApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IBinanceRestClientUsdFuturesSharedApi SharedApi { get; }

        /// <summary>
        ///Endpoints related to query user data.
        /// </summary>
        /// <see cref="IBinanceRestClientUsdFuturesApiAgent"/>
        public IBinanceRestClientUsdFuturesApiAgent Agent { get; }
    }
}

