using CryptoExchange.Net.Interfaces.Clients;

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
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IBinanceRestClientUsdFuturesApiShared SharedClient { get; }

        /// <summary>
        ///Endpoints related to query user data.
        /// </summary>
        /// <see cref="IBinanceRestClientUsdFuturesApiAgent"/>
        public IBinanceRestClientUsdFuturesApiAgent Agent { get; }
    }
}
