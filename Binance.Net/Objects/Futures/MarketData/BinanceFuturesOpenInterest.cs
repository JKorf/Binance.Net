using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Forced liquidation info
    /// </summary>
    public class BinanceFuturesOpenInterest
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Open Interest info
        /// </summary>
        public decimal OpenInterest { get; set; }
    }
}