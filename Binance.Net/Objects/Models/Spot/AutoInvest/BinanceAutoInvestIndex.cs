using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest index info
    /// </summary>
    public record BinanceAutoInvestIndex
    {
        /// <summary>
        /// Index id
        /// </summary>
        [JsonPropertyName("indexId")]
        public long IndexId { get; set; }
        /// <summary>
        /// Index name
        /// </summary>
        [JsonPropertyName("indexName")]
        public string IndexName { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestIndexStatus? Status { get; set; }
        /// <summary>
        /// Asset allocation
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
        /// Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Allocation
        /// </summary>
        [JsonPropertyName("allocation")]
        public decimal Allocation { get; set; }
    }


}
