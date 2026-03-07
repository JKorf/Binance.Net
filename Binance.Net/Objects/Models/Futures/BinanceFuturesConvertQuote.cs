namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Convert quote info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesConvertQuote
    {
        /// <summary>
        /// ["<c>quoteId</c>"] Quote id
        /// </summary>
        [JsonPropertyName("quoteId")]
        public string QuoteId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ratio</c>"] Ratio
        /// </summary>
        [JsonPropertyName("ratio")]
        public decimal Ratio { get; set; }
        /// <summary>
        /// ["<c>inverseRatio</c>"] Inverse ratio
        /// </summary>
        [JsonPropertyName("inverseRatio")]
        public decimal InverseRatio { get; set; }
        /// <summary>
        /// ["<c>validTimestamp</c>"] Until when the quote is valid
        /// </summary>
        [JsonPropertyName("validTimestamp")]
        public DateTime ValidTimestamp { get; set; }
        /// <summary>
        /// ["<c>toAmount</c>"] To quantity
        /// </summary>
        [JsonPropertyName("toAmount")]
        public decimal ToQuantity { get; set; }
        /// <summary>
        /// ["<c>fromAmount</c>"] From quantity
        /// </summary>
        [JsonPropertyName("fromAmount")]
        public decimal FromQuantity { get; set; }
    }


}

