using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Objects.Spot.MarketData;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Book price
    /// </summary>
    public class BinanceFuturesBookPrice: BinanceBookPrice
    {
        /// <summary>
        /// Pair
        /// </summary>
        public string Pair { get; set; } = "";
    }
}
