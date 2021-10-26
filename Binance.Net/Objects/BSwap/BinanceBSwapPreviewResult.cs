using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.BSwap
{
    /// <summary>
    /// Preview result
    /// </summary>
    public class BinanceBSwapPreviewResult
    {
        /// <summary>
        /// Quote asset
        /// </summary>
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("quoteAmt")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonProperty("baseAmt")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Share
        /// </summary>
        public decimal Share { get; set; }
        /// <summary>
        /// Slippage
        /// </summary>
        public decimal? Slippage { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal? Fee { get; set; }
    }
}
