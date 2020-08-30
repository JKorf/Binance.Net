using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketStream
{
    /// <summary>
    /// Index price update
    /// </summary>
    public class BinanceFuturesStreamIndexPrice: BinanceStreamEvent
    {
        /// <summary>
        /// The pair
        /// </summary>
        [JsonProperty("i")]
        public string Pair { get; set; } = "";
        /// <summary>
        /// The index price
        /// </summary>
        [JsonProperty("p")]
        public decimal IndexPrice { get; set; }
    }
}
