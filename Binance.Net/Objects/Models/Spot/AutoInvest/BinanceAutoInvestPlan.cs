using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Invest plan info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestPlan
    {
        /// <summary>
        /// Plan value in USD
        /// </summary>
        [JsonPropertyName("planValueInUSD")]
        public decimal PlanValueInUsd { get; set; }
        /// <summary>
        /// Plan value in BTC
        /// </summary>
        [JsonPropertyName("planValueInBTC")]
        public decimal PlanValueInBtc { get; set; }
        /// <summary>
        /// Profit and loss in USD
        /// </summary>
        [JsonPropertyName("pnlInUSD")]
        public decimal PnlInUsd { get; set; }
        /// <summary>
        /// Roi
        /// </summary>
        [JsonPropertyName("roi")]
        public decimal Roi { get; set; }
        /// <summary>
        /// Plans
        /// </summary>
        [JsonPropertyName("plans")]
        public BinanceAutoInvestPlanDetails[] Plans { get; set; } = Array.Empty<BinanceAutoInvestPlanDetails>();
    }

    /// <summary>
    /// Plan info
    /// </summary>
    public record BinanceAutoInvestPlanDetails
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
        /// Creation date time
        /// </summary>
        [JsonPropertyName("creationDateTime")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// First execution date time
        /// </summary>
        [JsonPropertyName("firstExecutionDateTime")]
        public DateTime? FirstExecutionTime { get; set; }
        /// <summary>
        /// Next execution date time
        /// </summary>
        [JsonPropertyName("nextExecutionDateTime")]
        public DateTime? NextExecutionTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestPlanStatus? Status { get; set; }
        /// <summary>
        /// Last updated date time
        /// </summary>
        [JsonPropertyName("lastUpdatedDateTime")]
        public DateTime? LastUpdateTime { get; set; }
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Total target quantity
        /// </summary>
        [JsonPropertyName("totalTargetAmount")]
        public decimal TotalTargetQuantity { get; set; }
        /// <summary>
        /// Source asset
        /// </summary>
        [JsonPropertyName("sourceAsset")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// Total invested in USD
        /// </summary>
        [JsonPropertyName("totalInvestedInUSD")]
        public decimal TotalInvestedInUsd { get; set; }
        /// <summary>
        /// Subscription quantity
        /// </summary>
        [JsonPropertyName("subscriptionAmount")]
        public decimal SubscriptionQuantity { get; set; }
        /// <summary>
        /// Subscription cycle
        /// </summary>
        [JsonPropertyName("subscriptionCycle")]
        public AutoInvestSubscriptionCycle SubscriptionCycle { get; set; }
        /// <summary>
        /// Subscription start day
        /// </summary>
        [JsonPropertyName("subscriptionStartDay")]
        public int? SubscriptionStartDay { get; set; }
        /// <summary>
        /// Subscription start weekday
        /// </summary>
        [JsonPropertyName("subscriptionStartWeekday")]
        public string SubscriptionStartWeekday { get; set; } = string.Empty;
        /// <summary>
        /// Subscription start time
        /// </summary>
        [JsonPropertyName("subscriptionStartTime")]
        public int? SubscriptionStartTime { get; set; }
        /// <summary>
        /// Source wallet
        /// </summary>
        [JsonPropertyName("sourceWallet")]
        public string SourceWallet { get; set; } = string.Empty;
        /// <summary>
        /// Flexible allowed to use
        /// </summary>
        [JsonPropertyName("flexibleAllowedToUse")]
        public bool FlexibleAllowedToUse { get; set; }
        /// <summary>
        /// Plan value in USD
        /// </summary>
        [JsonPropertyName("planValueInUSD")]
        public decimal? PlanValueInUsd { get; set; }
        /// <summary>
        /// Pnl in USD
        /// </summary>
        [JsonPropertyName("pnlInUSD")]
        public decimal? PnlInUsd { get; set; }
        /// <summary>
        /// Roi
        /// </summary>
        [JsonPropertyName("roi")]
        public decimal? Roi { get; set; }
    }


}
