namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Small liability history
    /// </summary>
    [SerializationModel]
    public record BinanceSmallLiabilityHistory
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>targetAsset</c>"] Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>targetAmount</c>"] Target quantity
        /// </summary>
        [JsonPropertyName("targetAmount")]
        public decimal TargetQuantity { get; set; }
        /// <summary>
        /// ["<c>bizType</c>"] Biz type
        /// </summary>
        [JsonPropertyName("bizType")]
        public string BizType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}

