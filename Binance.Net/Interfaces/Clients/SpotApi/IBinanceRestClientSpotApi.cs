using CryptoExchange.Net.Interfaces.CommonClients;

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
        public IBinanceRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        public IBinanceRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// DEPRECATED, use SharedClient instead
        /// </summary>
        public ISpotClient CommonSpotClient { get; }

        /// <summary>
        /// Get the shared rest requests client
        /// </summary>
        public IBinanceRestClientSpotApiShared SharedClient { get; }
    }
}
