using CryptoExchange.Net.Interfaces.Clients;
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
        /// <see cref="IBinanceRestClientCoinFuturesApiAccount"/>
        public IBinanceRestClientCoinFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        /// <see cref="IBinanceRestClientCoinFuturesApiExchangeData"/>
        public IBinanceRestClientCoinFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IBinanceRestClientCoinFuturesApiTrading"/>
        public IBinanceRestClientCoinFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IBinanceRestClientCoinFuturesApiShared SharedClient { get; }

        /// <summary>
        ///Endpoints related to query user data.
        /// </summary>
        /// <see cref="IBinanceRestClientCoinFuturesApiAgent"/>
        public IBinanceRestClientCoinFuturesApiAgent Agent { get; }

    }
}
