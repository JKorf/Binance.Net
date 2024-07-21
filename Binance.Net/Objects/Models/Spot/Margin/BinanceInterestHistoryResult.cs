namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Interest history entry info
    /// </summary>
    public record BinanceInterestHistory
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string? TransactionId { get; set; }
        /// <summary>
        /// Isolated symbol
        /// </summary>
        [JsonPropertyName("isolatedSymbol")]
        public string? IsolatedSymbol { get; set; }
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The raw asset
        /// </summary>
        [JsonPropertyName("rawAsset")]
        public string? RawAsset { get; set; }
        /// <summary>
        /// The quantity of interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("interestAccuredTime")]
        public DateTime InterestAccuredTime { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        [JsonPropertyName("principal")]
        public decimal Principal { get; set; }
        /// <summary>
        /// Type of interest
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
