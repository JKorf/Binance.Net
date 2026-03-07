namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking history
    /// </summary>
    [SerializationModel]
    public record BinanceStakingHistory
    {
        /// <summary>
        /// ["<c>asset</c>"] The staking asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] The staking timestamp.
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>distributeAsset</c>"] Distribute asset
        /// </summary>
        [JsonPropertyName("distributeAsset")]
        public string DistributedAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>distributeAmount</c>"] Quantity distributed
        /// </summary>
        [JsonPropertyName("distributeAmount")]
        public decimal DistributeQuantity { get; set; }
        /// <summary>
        /// ["<c>conversionRatio</c>"] Conversion ratio
        /// </summary>
        [JsonPropertyName("conversionRatio")]
        public decimal ConversionRatio { get; set; }
        [JsonInclude, JsonPropertyName("exchangeRate")]
        internal decimal ConversionRatioInt { get => ConversionRatio; set => ConversionRatio = value; }
    }
}

