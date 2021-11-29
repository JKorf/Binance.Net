using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces.Clients.Rest.UsdFutures
{
    /// <summary>
    /// Client for accessing the USD-M Binance futures API. 
    /// </summary>
    public interface IBinanceClientUsdFuturesMarket
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientUsdFuturesMarketAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceClientUsdFuturesMarketExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientUsdFuturesMarketTrading Trading { get; }
    }
}
