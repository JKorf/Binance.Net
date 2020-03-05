using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// TODO
    /// </summary>
    public class BinanceFuturesStreamMarkPrice: BinanceStreamEvent
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Mark Price
        /// </summary>
        [JsonProperty("p")]
        public decimal MarkPrice { get; set; }
        
        /// <summary>
        /// Next Funding Rate
        /// </summary>
        [JsonProperty("r")]
        public decimal FundingRate { get; set; }
        
        /// <summary>
        /// Next Funding Time
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime FundingTime { get; set; }
    }
}
