using CryptoExchange.Net.Interfaces.Clients;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot API endpoints
    /// </summary>
    public interface IBinanceRestClientSpotApi : IRestApiClient<BinanceCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IBinanceRestClientSpotApiAccount"/>
        public IBinanceRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IBinanceRestClientSpotApiExchangeData"/>
        public IBinanceRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IBinanceRestClientSpotApiTrading"/>
        public IBinanceRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// [V1] Get the shared rest requests client. For new implementations prefer using <see cref="SharedApi"/>
        /// </summary>
        public IBinanceRestClientSpotApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IBinanceRestClientSpotSharedApi SharedApi { get; }

        /// <summary>
        ///Endpoints related to query user data.
        /// </summary>
        /// <see cref="IBinanceRestClientSpotApiAgent"/>
        public IBinanceRestClientSpotApiAgent Agent { get; }
    }
}
