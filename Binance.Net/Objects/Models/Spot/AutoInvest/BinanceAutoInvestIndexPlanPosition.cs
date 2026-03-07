namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Plan position info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestIndexPlanPosition
    {
        /// <summary>
        /// ["<c>indexId</c>"] The index identifier.
        /// </summary>
        [JsonPropertyName("indexId")]
        public long IndexId { get; set; }
        /// <summary>
        /// ["<c>totalInvestedInUSD</c>"] Total invested in USD
        /// </summary>
        [JsonPropertyName("totalInvestedInUSD")]
        public decimal TotalInvestedInUsd { get; set; }
        /// <summary>
        /// ["<c>currentInvestedInUSD</c>"] Current invested in USD
        /// </summary>
        [JsonPropertyName("currentInvestedInUSD")]
        public decimal CurrentInvestedInUsd { get; set; }
        /// <summary>
        /// ["<c>pnlInUSD</c>"] Pnl in USD
        /// </summary>
        [JsonPropertyName("pnlInUSD")]
        public decimal PnlInUsd { get; set; }
        /// <summary>
        /// ["<c>roi</c>"] Return on investment.
        /// </summary>
        [JsonPropertyName("roi")]
        public decimal Roi { get; set; }
        /// <summary>
        /// ["<c>assetAllocation</c>"] Asset allocation
        /// </summary>
        [JsonPropertyName("assetAllocation")]
        public BinanceAutoInvestIndexPlanPositionAllocation[] AssetAllocation { get; set; } = Array.Empty<BinanceAutoInvestIndexPlanPositionAllocation>();
        /// <summary>
        /// ["<c>details</c>"] Details
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
        /// ["<c>targetAsset</c>"] Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>allocation</c>"] Allocation percentage
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
        /// ["<c>targetAsset</c>"] Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>averagePriceInUSD</c>"] Average price in USD
        /// </summary>
        [JsonPropertyName("averagePriceInUSD")]
        public decimal AveragePriceInUsd { get; set; }
        /// <summary>
        /// ["<c>totalInvestedInUSD</c>"] Total invested in USD
        /// </summary>
        [JsonPropertyName("totalInvestedInUSD")]
        public decimal TotalInvestedInUsd { get; set; }
        /// <summary>
        /// ["<c>currentInvestedInUSD</c>"] Current invested in USD
        /// </summary>
        [JsonPropertyName("currentInvestedInUSD")]
        public decimal CurrentInvestedInUsd { get; set; }
        /// <summary>
        /// ["<c>purchasedAmount</c>"] Purchased quantity
        /// </summary>
        [JsonPropertyName("purchasedAmount")]
        public decimal PurchasedQuantity { get; set; }
        /// <summary>
        /// ["<c>pnlInUSD</c>"] Pnl in USD
        /// </summary>
        [JsonPropertyName("pnlInUSD")]
        public decimal PnlInUsd { get; set; }
        /// <summary>
        /// ["<c>roi</c>"] Return on investment.
        /// </summary>
        [JsonPropertyName("roi")]
        public decimal Roi { get; set; }
        /// <summary>
        /// ["<c>percentage</c>"] Percentage
        /// </summary>
        [JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
        /// <summary>
        /// ["<c>availableAmount</c>"] Available quantity
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal AvailableQuantity { get; set; }
        /// <summary>
        /// ["<c>redeemedAmount</c>"] Redeemed quantity
        /// </summary>
        [JsonPropertyName("redeemedAmount")]
        public decimal RedeemedQuantity { get; set; }
        /// <summary>
        /// ["<c>assetValueInUSD</c>"] Asset value in USD
        /// </summary>
        [JsonPropertyName("assetValueInUSD")]
        public decimal AssetValueInUsd { get; set; }
    }


}

