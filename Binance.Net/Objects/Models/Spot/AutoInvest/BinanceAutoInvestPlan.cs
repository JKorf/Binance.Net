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
        /// ["<c>planValueInUSD</c>"] Plan value in USD
        /// </summary>
        [JsonPropertyName("planValueInUSD")]
        public decimal PlanValueInUsd { get; set; }
        /// <summary>
        /// ["<c>planValueInBTC</c>"] Plan value in BTC
        /// </summary>
        [JsonPropertyName("planValueInBTC")]
        public decimal PlanValueInBtc { get; set; }
        /// <summary>
        /// ["<c>pnlInUSD</c>"] Profit and loss in USD
        /// </summary>
        [JsonPropertyName("pnlInUSD")]
        public decimal PnlInUsd { get; set; }
        /// <summary>
        /// ["<c>roi</c>"] Return on investment.
        /// </summary>
        [JsonPropertyName("roi")]
        public decimal Roi { get; set; }
        /// <summary>
        /// ["<c>plans</c>"] Plans
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
        /// ["<c>creationDateTime</c>"] Creation date time
        /// </summary>
        [JsonPropertyName("creationDateTime")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ["<c>firstExecutionDateTime</c>"] First execution date time
        /// </summary>
        [JsonPropertyName("firstExecutionDateTime")]
        public DateTime? FirstExecutionTime { get; set; }
        /// <summary>
        /// ["<c>nextExecutionDateTime</c>"] Next execution date time
        /// </summary>
        [JsonPropertyName("nextExecutionDateTime")]
        public DateTime? NextExecutionTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestPlanStatus? Status { get; set; }
        /// <summary>
        /// ["<c>lastUpdatedDateTime</c>"] Last updated date time
        /// </summary>
        [JsonPropertyName("lastUpdatedDateTime")]
        public DateTime? LastUpdateTime { get; set; }
        /// <summary>
        /// ["<c>targetAsset</c>"] Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalTargetAmount</c>"] Total target quantity
        /// </summary>
        [JsonPropertyName("totalTargetAmount")]
        public decimal TotalTargetQuantity { get; set; }
        /// <summary>
        /// ["<c>sourceAsset</c>"] Source asset
        /// </summary>
        [JsonPropertyName("sourceAsset")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalInvestedInUSD</c>"] Total invested in USD
        /// </summary>
        [JsonPropertyName("totalInvestedInUSD")]
        public decimal TotalInvestedInUsd { get; set; }
        /// <summary>
        /// ["<c>subscriptionAmount</c>"] Subscription quantity
        /// </summary>
        [JsonPropertyName("subscriptionAmount")]
        public decimal SubscriptionQuantity { get; set; }
        /// <summary>
        /// ["<c>subscriptionCycle</c>"] Subscription cycle
        /// </summary>
        [JsonPropertyName("subscriptionCycle")]
        public AutoInvestSubscriptionCycle SubscriptionCycle { get; set; }
        /// <summary>
        /// ["<c>subscriptionStartDay</c>"] Subscription start day
        /// </summary>
        [JsonPropertyName("subscriptionStartDay")]
        public int? SubscriptionStartDay { get; set; }
        /// <summary>
        /// ["<c>subscriptionStartWeekday</c>"] Subscription start weekday
        /// </summary>
        [JsonPropertyName("subscriptionStartWeekday")]
        public string SubscriptionStartWeekday { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>subscriptionStartTime</c>"] Subscription start time
        /// </summary>
        [JsonPropertyName("subscriptionStartTime")]
        public int? SubscriptionStartTime { get; set; }
        /// <summary>
        /// ["<c>sourceWallet</c>"] Source wallet
        /// </summary>
        [JsonPropertyName("sourceWallet")]
        public string SourceWallet { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>flexibleAllowedToUse</c>"] Flexible allowed to use
        /// </summary>
        [JsonPropertyName("flexibleAllowedToUse")]
        public bool FlexibleAllowedToUse { get; set; }
        /// <summary>
        /// ["<c>planValueInUSD</c>"] Plan value in USD
        /// </summary>
        [JsonPropertyName("planValueInUSD")]
        public decimal? PlanValueInUsd { get; set; }
        /// <summary>
        /// ["<c>pnlInUSD</c>"] Pnl in USD
        /// </summary>
        [JsonPropertyName("pnlInUSD")]
        public decimal? PnlInUsd { get; set; }
        /// <summary>
        /// ["<c>roi</c>"] Return on investment.
        /// </summary>
        [JsonPropertyName("roi")]
        public decimal? Roi { get; set; }
    }


}

