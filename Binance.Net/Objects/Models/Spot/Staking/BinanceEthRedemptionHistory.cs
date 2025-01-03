namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Redemption history
    /// </summary>
    public record BinanceEthRedemptionHistory
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Arrival timestamp
        /// </summary>
        [JsonPropertyName("arrivalTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// Distribute asset
        /// </summary>
        [JsonPropertyName("distributeAsset")]
        public string DistributedAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity distributed
        /// </summary>
        [JsonPropertyName("distributeAmount")]
        public decimal DistributeQuantity { get; set; }
        /// <summary>
        /// Conversion ratio
        /// </summary>
        [JsonPropertyName("conversionRatio")]
        public decimal ConversionRatio { get; set; }
    }
}
