using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Interfaces.CommonClients;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Binance Coin futures API endpoints
    /// </summary>
    public interface IBinanceRestClientCoinFuturesApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceRestClientCoinFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceRestClientCoinFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceRestClientCoinFuturesApiTrading Trading { get; }

        /// <summary>
        /// DEPRECATED, use SharedClient instead
        /// </summary>
        public IFuturesClient CommonFuturesClient { get; }

        /// <summary>
        /// Get the shared rest requests client
        /// </summary>
        public IBinanceRestClientCoinFuturesApiShared SharedClient { get; }

    }
}
