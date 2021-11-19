using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces.Clients.Rest.CoinFutures
{
    /// <summary>
    /// Client for accessing the COIN-M Binance futures API. 
    /// </summary>
    public interface IBinanceClientCoinFutures: IRestClient
    {
        /// <summary>
        /// Set the API creentials to use for this client
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="apiSecret">The API secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);

        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientCoinFuturesAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceClientCoinFuturesExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientCoinFuturesTrading Trading { get; }
    }
}
