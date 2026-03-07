using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest index info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestIndex
    {
        /// <summary>
        /// ["<c>indexId</c>"] The index identifier.
        /// </summary>
        [JsonPropertyName("indexId")]
        public long IndexId { get; set; }
        /// <summary>
        /// ["<c>indexName</c>"] Index name
        /// </summary>
        [JsonPropertyName("indexName")]
        public string IndexName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] The index status.
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestIndexStatus? Status { get; set; }
        /// <summary>
        /// ["<c>assetAllocation</c>"] Asset allocation
        /// </summary>
        [JsonPropertyName("assetAllocation")]
        public BinanceAutoInvestAssetIndex[] AssetAllocation { get; set; } = Array.Empty<BinanceAutoInvestAssetIndex>();
    }

    /// <summary>
    /// Allocation
    /// </summary>
    public record BinanceAutoInvestAssetIndex
    {
        /// <summary>
        /// ["<c>targetAsset</c>"] Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>allocation</c>"] Allocation percentage.
        /// </summary>
        [JsonPropertyName("allocation")]
        public decimal Allocation { get; set; }
    }


}

