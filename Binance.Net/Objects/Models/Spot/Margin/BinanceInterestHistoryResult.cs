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
        public string? IsolatedSymbol { get; set; }
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The raw asset
        /// </summary>
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
        public DateTime InterestAccuredTime { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Type of interest
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
