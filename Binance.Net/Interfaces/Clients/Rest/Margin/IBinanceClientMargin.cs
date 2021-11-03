using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.Rest.Margin
{
    /// <summary>
    /// Margin endpoints
    /// </summary>
    public interface IBinanceClientMargin : IRestClient
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientMarginAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceClientMarginMarketData MarketData { get; }
        /// <summary>
        /// Endpoints related to system information
        /// </summary>
        public IBinanceClientMarginSystemInfo SystemInfo { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientMarginTrading Trading { get; }
    }
}
