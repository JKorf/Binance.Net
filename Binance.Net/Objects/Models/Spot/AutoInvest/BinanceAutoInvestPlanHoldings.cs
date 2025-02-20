using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Plan holdings
    /// </summary>
    public record BinanceAutoInvestPlanHoldings
    {
        /// <summary>
        /// Plan id
        /// </summary>
        [JsonPropertyName("planId")]
        public long PlanId { get; set; }
        /// <summary>
        /// Plan type
        /// </summary>
        [JsonPropertyName("planType")]
        public AutoInvestPlanType PlanType { get; set; }
        /// <summary>
        /// Edit allowed
        /// </summary>
        [JsonPropertyName("editAllowed")]
        public bool EditAllowed { get; set; }
        /// <summary>
        /// Flexible allowed to use
        /// </summary>
        [JsonPropertyName("flexibleAllowedToUse")]
        public bool FlexibleAllowedToUse { get; set; }
        /// <summary>
        /// Creation date time
        /// </summary>
        [JsonPropertyName("creationDateTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// First execution date time
        /// </summary>
        [JsonPropertyName("firstExecutionDateTime")]
        public DateTime FirstExecutionTime { get; set; }
        /// <summary>
        /// Next execution date time
        /// </summary>
        [JsonPropertyName("nextExecutionDateTime")]
        public DateTime? NextExecutionTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestPlanStatus Status { get; set; }
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Source asset
        /// </summary>
        [JsonPropertyName("sourceAsset")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// Plan value in USD
        /// </summary>
        [JsonPropertyName("planValueInUSD")]
        public decimal PlanValueInUsd { get; set; }
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
        /// Total invested in USD
        /// </summary>
        [JsonPropertyName("totalInvestedInUSD")]
        public decimal TotalInvestedInUsd { get; set; }
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("details")]
        public IEnumerable<BinanceAutoInvestPlanHoldingDetails> Details { get; set; } = Array.Empty<BinanceAutoInvestPlanHoldingDetails>();
    }

    /// <summary>
    /// Holding details
    /// </summary>
    public record BinanceAutoInvestPlanHoldingDetails
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
        /// Purchased quantity
        /// </summary>
        [JsonPropertyName("purchasedAmount")]
        public decimal PurchasedQuantity { get; set; }
        /// <summary>
        /// Purchased quantity asset
        /// </summary>
        [JsonPropertyName("purchasedAmountUnit")]
        public string PurchasedQuantityAsset { get; set; } = string.Empty;
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
        /// Asset status
        /// </summary>
        [JsonPropertyName("assetStatus")]
        public string AssetStatus { get; set; } = string.Empty;
        /// <summary>
        /// Available quantity
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal? AvailableQuantity { get; set; }
        /// <summary>
        /// Available quantity unit
        /// </summary>
        [JsonPropertyName("availableAmountUnit")]
        public string? AvailableQuantityUnit { get; set; }
        /// <summary>
        /// Redeemed amount
        /// </summary>
        [JsonPropertyName("redeemedAmout")]
        public decimal? RedeemedAmount { get; set; }
        /// <summary>
        /// Redeemed amount asset
        /// </summary>
        [JsonPropertyName("redeemedAmoutUnit")]
        public string? RedeemedAmountAsset { get; set; }
        /// <summary>
        /// Asset value in USD
        /// </summary>
        [JsonPropertyName("assetValueInUSD")]
        public decimal? AssetValueInUsd { get; set; }
    }


}
