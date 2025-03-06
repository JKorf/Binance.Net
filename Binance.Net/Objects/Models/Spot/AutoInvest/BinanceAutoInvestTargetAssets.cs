namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest source asset info
    /// </summary>
    public record BinanceAutoInvestTargetAssets
    {
        /// <summary>
        /// Target assets
        /// </summary>
        [JsonPropertyName("targetAssets")]
        public string[] TargetAssets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Target asset list
        /// </summary>
        [JsonPropertyName("autoInvestAssetList")]
        public BinanceAutoInvestTargetAsset[] Assets { get; set; } = Array.Empty<BinanceAutoInvestTargetAsset>();
    }

    /// <summary>
    /// Auto invest target asset
    /// </summary>
    public record BinanceAutoInvestTargetAsset
    {
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Target asset list
        /// </summary>
        [JsonPropertyName("roiAndDimensionTypeList")]
        public BinanceAutoInvestTargetAssetRoi[] Assets { get; set; } = Array.Empty<BinanceAutoInvestTargetAssetRoi>();
    }

    /// <summary>
    /// Auto invest target asset roi
    /// </summary>
    public record BinanceAutoInvestTargetAssetRoi
    {
        /// <summary>
        /// Simulate ROI
        /// </summary>
        [JsonPropertyName("simulateRoi")]
        public decimal SimulateRoi { get; set; }
        /// <summary>
        /// The dimension
        /// </summary>
        [JsonPropertyName("dimensionValue")]
        public decimal DimensionValue { get; set; }
        /// <summary>
        /// The dimension unit
        /// </summary>
        [JsonPropertyName("dimensionUnit")]
        public string DimensionUnit { get; set; } = string.Empty;
    }
}
