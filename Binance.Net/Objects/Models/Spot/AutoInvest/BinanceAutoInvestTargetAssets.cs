namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest source asset info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestTargetAssets
    {
        /// <summary>
        /// ["<c>targetAssets</c>"] Available target assets.
        /// </summary>
        [JsonPropertyName("targetAssets")]
        public string[] TargetAssets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>autoInvestAssetList</c>"] Target asset list
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
        /// ["<c>targetAsset</c>"] Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>roiAndDimensionTypeList</c>"] ROI and dimension data for the target asset.
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
        /// ["<c>simulateRoi</c>"] Simulate ROI
        /// </summary>
        [JsonPropertyName("simulateRoi")]
        public decimal SimulateRoi { get; set; }
        /// <summary>
        /// ["<c>dimensionValue</c>"] The dimension
        /// </summary>
        [JsonPropertyName("dimensionValue")]
        public decimal DimensionValue { get; set; }
        /// <summary>
        /// ["<c>dimensionUnit</c>"] The dimension unit
        /// </summary>
        [JsonPropertyName("dimensionUnit")]
        public string DimensionUnit { get; set; } = string.Empty;
    }
}

