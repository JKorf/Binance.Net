namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert Quote
    /// </summary>
    [SerializationModel]
    public record BinanceConvertQuote
    {
        /// <summary>
        /// ["<c>quoteId</c>"] Quote id
        /// </summary>
        [JsonPropertyName("quoteId")]
        public string? QuoteId { get; set; }
        /// <summary>
        /// ["<c>ratio</c>"] Price ratio
        /// </summary>
        [JsonPropertyName("ratio")]
        public decimal Ratio { get; set; }
        /// <summary>
        /// ["<c>inverseRatio</c>"] Inverse price ratio
        /// </summary>
        [JsonPropertyName("inverseRatio")]
        public decimal InverseRatio { get; set; }
        /// <summary>
        /// ["<c>validTimestamp</c>"] Valid Timestamp
        /// </summary>
        [JsonPropertyName("validTimestamp")]
        public long ValidTimestamp { get; set; }
        /// <summary>
        /// ["<c>toAmount</c>"] Base quantity
        /// </summary>
        [JsonPropertyName("toAmount")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// ["<c>fromAmount</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("fromAmount")]
        public decimal QuoteQuantity { get; set; }

    }
}

