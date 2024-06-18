namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Small liability history
    /// </summary>
    public record BinanceSmallLiabilityHistory
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonProperty("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Target quantity
        /// </summary>
        [JsonProperty("targetAmount")]
        public decimal TargetQuantity { get; set; }
        /// <summary>
        /// Biz type
        /// </summary>
        [JsonProperty("bizType")]
        public string BizType { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
