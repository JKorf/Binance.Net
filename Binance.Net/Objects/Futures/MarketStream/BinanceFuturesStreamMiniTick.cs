using System;
using System.Collections.Generic;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketStream
{
    /// <summary>
    /// TODO
    /// </summary>
    public class BinanceFuturesStreamMiniTick : IBinanceMiniTick
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
        public decimal LastPrice { get; set; }
        
        /// <summary>
        /// Open Price
        /// </summary>
        [JsonProperty("o")]
        public decimal OpenPrice { get; set; }
        
        /// <summary>
        /// High
        /// </summary>
        [JsonProperty("h")]
        public decimal HighPrice { get; set; }
        
        /// <summary>
        /// Low
        /// </summary>
        [JsonProperty("l")]
        public decimal LowPrice { get; set; }
        
        /// <summary>
        /// Total traded base asset volume
        /// </summary>
        [JsonProperty("v")]
        public decimal TotalTradedBaseAssetVolume { get; set; }
        
        /// <summary>
        /// Total traded quote asset volume
        /// </summary>
        [JsonProperty("q")]
        public decimal TotalTradedQuoteAssetVolume { get; set; }
        
    }
}
