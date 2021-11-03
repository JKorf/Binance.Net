using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.Rest.UsdFutures
{
    /// <summary>
    /// Usd futures
    /// </summary>
    public interface IBinanceClientUsdFutures : IRestClient
    {

        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientUsdFuturesAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceClientUsdFuturesMarketData MarketData { get; }
        /// <summary>
        /// Endpoints related to system information
        /// </summary>
        public IBinanceClientUsdFuturesSystemInfo SystemInfo { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientUsdFuturesTrading Trading { get; }
    }
}
