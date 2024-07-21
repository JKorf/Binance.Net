namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert Quote
    /// </summary>
    public record BinanceConvertQuote
    {
        /// <summary>
        /// Quote id
        /// </summary>
        [JsonPropertyName("quoteId")]
        public string? QuoteId { get; set; }
        /// <summary>
        /// Price ratio
        /// </summary>
        [JsonPropertyName("ratio")]
        public decimal Ratio { get; set; }
        /// <summary>
        /// Inverse price ratio
        /// </summary>
        [JsonPropertyName("inverseRatio")]
        public decimal InverseRatio { get; set; }
        /// <summary>
        /// Valid Timestamp
        /// </summary>
        [JsonPropertyName("validTimestamp")]
        public long ValidTimestamp { get; set; }
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonPropertyName("toAmount")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonPropertyName("fromAmount")]
        public decimal QuoteQuantity { get; set; }
       
    }
}
