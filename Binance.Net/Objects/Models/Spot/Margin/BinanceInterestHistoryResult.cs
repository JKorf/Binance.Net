namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Interest history entry info
    /// </summary>
    [SerializationModel]
    public record BinanceInterestHistory
    {
        /// <summary>
        /// ["<c>txId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public long? TransactionId { get; set; }
        /// <summary>
        /// ["<c>isolatedSymbol</c>"] Isolated symbol
        /// </summary>
        [JsonPropertyName("isolatedSymbol")]
        public string? IsolatedSymbol { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] The asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rawAsset</c>"] The raw asset
        /// </summary>
        [JsonPropertyName("rawAsset")]
        public string? RawAsset { get; set; }
        /// <summary>
        /// ["<c>interest</c>"] The quantity of interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// ["<c>interestAccuredTime</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("interestAccuredTime")]
        public DateTime InterestAccuredTime { get; set; }
        /// <summary>
        /// ["<c>interestRate</c>"] Interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// ["<c>principal</c>"] Principal
        /// </summary>
        [JsonPropertyName("principal")]
        public decimal Principal { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type of interest
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}

