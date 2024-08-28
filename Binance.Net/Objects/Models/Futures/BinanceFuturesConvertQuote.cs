using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Convert quote info
    /// </summary>
    public record BinanceFuturesConvertQuote
    {
        /// <summary>
        /// Quote id
        /// </summary>
        [JsonPropertyName("quoteId")]
        public string QuoteId { get; set; } = string.Empty;
        /// <summary>
        /// Ratio
        /// </summary>
        [JsonPropertyName("ratio")]
        public decimal Ratio { get; set; }
        /// <summary>
        /// Inverse ratio
        /// </summary>
        [JsonPropertyName("inverseRatio")]
        public decimal InverseRatio { get; set; }
        /// <summary>
        /// Until when the quote is valid
        /// </summary>
        [JsonPropertyName("validTimestamp")]
        public DateTime ValidTimestamp { get; set; }
        /// <summary>
        /// To quantity
        /// </summary>
        [JsonPropertyName("toAmount")]
        public decimal ToQuantity { get; set; }
        /// <summary>
        /// From quantity
        /// </summary>
        [JsonPropertyName("fromAmount")]
        public decimal FromQuantity { get; set; }
    }


}
