using System;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD futures API endpoints
    /// </summary>
    public interface IBinanceClientUsdFuturesApi : IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientUsdFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceClientUsdFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientUsdFuturesApiTrading Trading { get; }
    }
}
