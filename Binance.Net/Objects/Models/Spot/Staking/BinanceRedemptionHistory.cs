namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Redemption history
    /// </summary>
    [SerializationModel]
    public record BinanceRedemptionHistory
    {
        /// <summary>
        /// ["<c>asset</c>"] The redemption asset.
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
        /// ["<c>time</c>"] The redemption timestamp.
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>arrivalTime</c>"] Arrival timestamp
        /// </summary>
        [JsonPropertyName("arrivalTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ArrivalTime { get; set; }
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

