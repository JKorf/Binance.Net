namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Small liability asset
    /// </summary>
    public record BinanceSmallLiabilityAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        [JsonPropertyName("principal")]
        public decimal Principal { get; set; }
        /// <summary>
        /// Liability asset
        /// </summary>
        [JsonPropertyName("liabilityAsset")]
        public string LiabilityAsset { get; set; } = string.Empty;
        /// <summary>
        /// Liability quantity
        /// </summary>
        [JsonPropertyName("liabilityQty")]
        public decimal LiabilityQuantity { get; set; }
    }
}
