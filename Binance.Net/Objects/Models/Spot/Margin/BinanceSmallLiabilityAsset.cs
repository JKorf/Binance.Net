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
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Interest
        /// </summary>
        [JsonProperty("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        [JsonProperty("principal")]
        public decimal Principal { get; set; }
        /// <summary>
        /// Liability asset
        /// </summary>
        [JsonProperty("liabilityAsset")]
        public string LiabilityAsset { get; set; } = string.Empty;
        /// <summary>
        /// Liability quantity
        /// </summary>
        [JsonProperty("liabilityQty")]
        public decimal LiabilityQuantity { get; set; }
    }
}
