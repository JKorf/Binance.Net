using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Binance.Net.Objects.BSwap
{
    /// <summary>
    /// Quote info
    /// </summary>
    public class BinanceBSwapQuote
    {
        /// <summary>
        /// Quote asset
        /// </summary>
        public string QuoteAsset { get; set; } = "";
        /// <summary>
        /// Base asset
        /// </summary>
        public string BaseAsset { get; set; } = "";
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonProperty("baseQty")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Slippage
        /// </summary>
        public decimal Slippage { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
    }
}
