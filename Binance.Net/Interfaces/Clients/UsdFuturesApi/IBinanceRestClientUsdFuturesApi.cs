using CryptoExchange.Net.Interfaces.CommonClients;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD futures API endpoints
    /// </summary>
    public interface IBinanceRestClientUsdFuturesApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceRestClientUsdFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceRestClientUsdFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceRestClientUsdFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the IFuturesClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
        /// </summary>
        /// <returns></returns>
        public IFuturesClient CommonFuturesClient { get; }

        public IBinanceRestClientUsdFuturesApiShared SharedClient { get; }
    }
}
