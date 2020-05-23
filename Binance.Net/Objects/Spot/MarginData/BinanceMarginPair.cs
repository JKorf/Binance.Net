﻿using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// Margin pair info
    /// </summary>
    public class BinanceMarginPair
    {
        /// <summary>
        /// Base asset of the pair
        /// </summary>
        [JsonProperty("base")]
        public string BaseAsset { get; set; } = "";
        /// <summary>
        /// Quote asset of the pair
        /// </summary>
        [JsonProperty("quote")]
        public string QuoteAsset { get; set; } = "";
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Is buying allowed
        /// </summary>
        public bool IsBuyAllowed { get; set; }
        /// <summary>
        /// Is selling allowed
        /// </summary>
        public bool IsSellAllowed { get; set; }
        /// <summary>
        /// Is margin trading
        /// </summary>
        public bool IsMarginTrade { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        public string Symbol { get; set; } = "";
    }
}
