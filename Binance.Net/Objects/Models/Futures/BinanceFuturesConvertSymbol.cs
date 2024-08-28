using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Convert symbol info
    /// </summary>
    public record BinanceFuturesConvertSymbol
    {
        /// <summary>
        /// From asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// To asset
        /// </summary>
        [JsonPropertyName("toAsset")]
        public string ToAsset { get; set; } = string.Empty;
        /// <summary>
        /// Minimal convert from asset quantity
        /// </summary>
        [JsonPropertyName("fromAssetMinAmount")]
        public decimal FromAssetMinQuantity { get; set; }
        /// <summary>
        /// Maximal convert from asset quantity
        /// </summary>
        [JsonPropertyName("fromAssetMaxAmount")]
        public decimal FromAssetMaxQuantity { get; set; }
        /// <summary>
        /// Minimal convert to asset quantity
        /// </summary>
        [JsonPropertyName("toAssetMinAmount")]
        public decimal ToAssetMinQuantity { get; set; }
        /// <summary>
        /// Maximal convert to asset quantity
        /// </summary>
        [JsonPropertyName("toAssetMaxAmount")]
        public decimal ToAssetMaxQuantity { get; set; }
    }


}
