using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Plan holdings
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestPlanHoldings
    {
        /// <summary>
        /// ["<c>planId</c>"] The plan identifier.
        /// </summary>
        [JsonPropertyName("planId")]
        public long PlanId { get; set; }
        /// <summary>
        /// ["<c>planType</c>"] Plan type
        /// </summary>
        [JsonPropertyName("planType")]
        public AutoInvestPlanType PlanType { get; set; }
        /// <summary>
        /// ["<c>editAllowed</c>"] Edit allowed
        /// </summary>
        [JsonPropertyName("editAllowed")]
        public bool EditAllowed { get; set; }
        /// <summary>
        /// ["<c>flexibleAllowedToUse</c>"] Flexible allowed to use
        /// </summary>
        [JsonPropertyName("flexibleAllowedToUse")]
        public bool FlexibleAllowedToUse { get; set; }
        /// <summary>
        /// ["<c>creationDateTime</c>"] Creation date time
        /// </summary>
        [JsonPropertyName("creationDateTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>firstExecutionDateTime</c>"] First execution date time
        /// </summary>
        [JsonPropertyName("firstExecutionDateTime")]
        public DateTime FirstExecutionTime { get; set; }
        /// <summary>
        /// ["<c>nextExecutionDateTime</c>"] Next execution date time
        /// </summary>
        [JsonPropertyName("nextExecutionDateTime")]
        public DateTime? NextExecutionTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestPlanStatus Status { get; set; }
        /// <summary>
        /// ["<c>targetAsset</c>"] Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sourceAsset</c>"] Source asset
        /// </summary>
        [JsonPropertyName("sourceAsset")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>planValueInUSD</c>"] Plan value in USD
        /// </summary>
        [JsonPropertyName("planValueInUSD")]
        public decimal PlanValueInUsd { get; set; }
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
        /// ["<c>totalInvestedInUSD</c>"] Total invested in USD
        /// </summary>
        [JsonPropertyName("totalInvestedInUSD")]
        public decimal TotalInvestedInUsd { get; set; }
        /// <summary>
        /// ["<c>details</c>"] Details
        /// </summary>
        [JsonPropertyName("details")]
        public BinanceAutoInvestPlanHoldingDetails[] Details { get; set; } = Array.Empty<BinanceAutoInvestPlanHoldingDetails>();
    }

    /// <summary>
    /// Holding details
    /// </summary>
    public record BinanceAutoInvestPlanHoldingDetails
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
        /// ["<c>purchasedAmount</c>"] Purchased quantity
        /// </summary>
        [JsonPropertyName("purchasedAmount")]
        public decimal PurchasedQuantity { get; set; }
        /// <summary>
        /// ["<c>purchasedAmountUnit</c>"] Purchased quantity asset
        /// </summary>
        [JsonPropertyName("purchasedAmountUnit")]
        public string PurchasedQuantityAsset { get; set; } = string.Empty;
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
        /// ["<c>assetStatus</c>"] Asset status
        /// </summary>
        [JsonPropertyName("assetStatus")]
        public string AssetStatus { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>availableAmount</c>"] Available quantity
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal? AvailableQuantity { get; set; }
        /// <summary>
        /// ["<c>availableAmountUnit</c>"] Available quantity unit
        /// </summary>
        [JsonPropertyName("availableAmountUnit")]
        public string? AvailableQuantityUnit { get; set; }
        /// <summary>
        /// ["<c>redeemedAmout</c>"] Redeemed amount
        /// </summary>
        [JsonPropertyName("redeemedAmout")]
        public decimal? RedeemedAmount { get; set; }
        /// <summary>
        /// ["<c>redeemedAmoutUnit</c>"] Redeemed amount asset
        /// </summary>
        [JsonPropertyName("redeemedAmoutUnit")]
        public string? RedeemedAmountAsset { get; set; }
        /// <summary>
        /// ["<c>assetValueInUSD</c>"] Asset value in USD
        /// </summary>
        [JsonPropertyName("assetValueInUSD")]
        public decimal? AssetValueInUsd { get; set; }
    }


}

