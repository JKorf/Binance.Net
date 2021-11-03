using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.Rest.Margin
{
    /// <summary>
    /// Coin futures endpoints
    /// </summary>
    public interface IBinanceClientCoinFutures: IRestClient
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientCoinFuturesAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceClientCoinFuturesMarketData MarketData { get; }
        /// <summary>
        /// Endpoints related to system information
        /// </summary>
        public IBinanceClientCoinFuturesSystemInfo SystemInfo { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientCoinFuturesTrading Trading { get; }
    }
}
