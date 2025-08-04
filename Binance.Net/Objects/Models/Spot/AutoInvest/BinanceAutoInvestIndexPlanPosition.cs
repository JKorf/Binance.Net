namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Plan position info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestIndexPlanPosition
    {
        /// <summary>
        /// Index id
        /// </summary>
        [JsonPropertyName("indexId")]
        public long IndexId { get; set; }
        /// <summary>
        /// Total invested in USD
        /// </summary>
        [JsonPropertyName("totalInvestedInUSD")]
        public decimal TotalInvestedInUsd { get; set; }
        /// <summary>
        /// Current invested in USD
        /// </summary>
        [JsonPropertyName("currentInvestedInUSD")]
        public decimal CurrentInvestedInUsd { get; set; }
        /// <summary>
        /// Pnl in USD
        /// </summary>
        [JsonPropertyName("pnlInUSD")]
        public decimal PnlInUsd { get; set; }
        /// <summary>
        /// Roi
        /// </summary>
        [JsonPropertyName("roi")]
        public decimal Roi { get; set; }
        /// <summary>
        /// Asset allocation
        /// </summary>
        [JsonPropertyName("assetAllocation")]
        public BinanceAutoInvestIndexPlanPositionAllocation[] AssetAllocation { get; set; } = Array.Empty<BinanceAutoInvestIndexPlanPositionAllocation>();
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("details")]
        public BinanceAutoInvestIndexPlanPositionDetails[] Details { get; set; } = Array.Empty<BinanceAutoInvestIndexPlanPositionDetails>();
    }

    /// <summary>
    /// Asset allocation info
    /// </summary>
    public record BinanceAutoInvestIndexPlanPositionAllocation
    {
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Allocation percentage
        /// </summary>
        [JsonPropertyName("allocation")]
        public decimal Allocation { get; set; }
    }

    /// <summary>
    /// Position details
    /// </summary>
    public record BinanceAutoInvestIndexPlanPositionDetails
    {
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Average price in USD
        /// </summary>
        [JsonPropertyName("averagePriceInUSD")]
        public decimal AveragePriceInUsd { get; set; }
        /// <summary>
        /// Total invested in USD
        /// </summary>
        [JsonPropertyName("totalInvestedInUSD")]
        public decimal TotalInvestedInUsd { get; set; }
        /// <summary>
        /// Current invested in USD
        /// </summary>
        [JsonPropertyName("currentInvestedInUSD")]
        public decimal CurrentInvestedInUsd { get; set; }
        /// <summary>
        /// Purchased quantity
        /// </summary>
        [JsonPropertyName("purchasedAmount")]
        public decimal PurchasedQuantity { get; set; }
        /// <summary>
        /// Pnl in USD
        /// </summary>
        [JsonPropertyName("pnlInUSD")]
        public decimal PnlInUsd { get; set; }
        /// <summary>
        /// Roi
        /// </summary>
        [JsonPropertyName("roi")]
        public decimal Roi { get; set; }
        /// <summary>
        /// Percentage
        /// </summary>
        [JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
        /// <summary>
        /// Available quantity
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal AvailableQuantity { get; set; }
        /// <summary>
        /// Redeemed quantity
        /// </summary>
        [JsonPropertyName("redeemedAmount")]
        public decimal RedeemedQuantity { get; set; }
        /// <summary>
        /// Asset value in USD
        /// </summary>
        [JsonPropertyName("assetValueInUSD")]
        public decimal AssetValueInUsd { get; set; }
    }


}
