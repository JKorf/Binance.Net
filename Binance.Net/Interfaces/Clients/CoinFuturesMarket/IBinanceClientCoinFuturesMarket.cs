using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces.Clients.Rest.CoinFutures
{
    /// <summary>
    /// Client for accessing the COIN-M Binance futures API. 
    /// </summary>
    public interface IBinanceClientCoinFuturesMarket
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientCoinFuturesMarketAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceClientCoinFuturesMarketExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientCoinFuturesMarketTrading Trading { get; }
    }
}
