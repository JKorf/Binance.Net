using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// TODO
    /// </summary>
    public class BinanceFuturesStreamMiniTick
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Close Price
        /// </summary>
        [JsonProperty("c")]
        public decimal Close { get; set; }
        
        /// <summary>
        /// Open Price
        /// </summary>
        [JsonProperty("o")]
        public decimal Open { get; set; }
        
        /// <summary>
        /// High
        /// </summary>
        [JsonProperty("h")]
        public decimal High { get; set; }
        
        /// <summary>
        /// Low
        /// </summary>
        [JsonProperty("l")]
        public decimal Low { get; set; }
        
        /// <summary>
        /// Total traded base asset volume
        /// </summary>
        [JsonProperty("v")]
        public decimal BaseVolume { get; set; }
        
        /// <summary>
        /// Total traded quote asset volume
        /// </summary>
        [JsonProperty("q")]
        public decimal QuoteVolume { get; set; }
        
    }
}
