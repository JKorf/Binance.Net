namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Small liability asset
    /// </summary>
    [SerializationModel]
    public record BinanceSmallLiabilityAsset
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>interest</c>"] Interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// ["<c>principal</c>"] Principal
        /// </summary>
        [JsonPropertyName("principal")]
        public decimal Principal { get; set; }
        /// <summary>
        /// ["<c>liabilityAsset</c>"] Liability asset
        /// </summary>
        [JsonPropertyName("liabilityAsset")]
        public string LiabilityAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>liabilityQty</c>"] Liability quantity
        /// </summary>
        [JsonPropertyName("liabilityQty")]
        public decimal LiabilityQuantity { get; set; }
    }
}

