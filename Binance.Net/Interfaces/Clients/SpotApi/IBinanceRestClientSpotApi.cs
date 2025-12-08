using CryptoExchange.Net.Interfaces.Clients;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot API endpoints
    /// </summary>
    public interface IBinanceRestClientSpotApi : IRestApiClient, IDisposable
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
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IBinanceRestClientSpotApiShared SharedClient { get; }

        /// <summary>
        ///Endpoints related to query user data.
        /// </summary>
        /// <see cref="IBinanceRestClientSpotApiAgent"/>
        public IBinanceRestClientSpotApiAgent Agent { get; }
    }
}
