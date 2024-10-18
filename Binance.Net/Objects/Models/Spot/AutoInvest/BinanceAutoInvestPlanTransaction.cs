using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Transactions
    /// </summary>
    public record BinanceAutoInvestPlanTransactions
    {
        /// <summary>
        /// Total
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// Results
        /// </summary>
        [JsonPropertyName("list")]
        public IEnumerable<BinanceAutoInvestPlanTransaction> List { get; set; } = Array.Empty<BinanceAutoInvestPlanTransaction>();
    }

    /// <summary>
    /// Plan transaction info
    /// </summary>
    public record BinanceAutoInvestPlanTransaction
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Plan type
        /// </summary>
        [JsonPropertyName("planType")]
        public AutoInvestPlanType PlanType { get; set; }
        /// <summary>
        /// Plan name
        /// </summary>
        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = string.Empty;
        /// <summary>
        /// Plan id
        /// </summary>
        [JsonPropertyName("planId")]
        public long PlanId { get; set; }
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonPropertyName("transactionDateTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// Transaction status
        /// </summary>
        [JsonPropertyName("transactionStatus")]
        public AutoInvestTransactionStatus TransactionStatus { get; set; }
        /// <summary>
        /// Failed type
        /// </summary>
        [JsonPropertyName("failedType")]
        public string? FailedType { get; set; }
        /// <summary>
        /// Source asset
        /// </summary>
        [JsonPropertyName("sourceAsset")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// Source asset quantity
        /// </summary>
        [JsonPropertyName("sourceAssetAmount")]
        public decimal SourceAssetQuantity { get; set; }
        /// <summary>
        /// Target asset quantity
        /// </summary>
        [JsonPropertyName("targetAssetAmount")]
        public decimal TargetAssetQuantity { get; set; }
        /// <summary>
        /// Source wallet
        /// </summary>
        [JsonPropertyName("sourceWallet")]
        public string SourceWallet { get; set; } = string.Empty;
        /// <summary>
        /// Flexible used
        /// </summary>
        [JsonPropertyName("flexibleUsed")]
        public bool FlexibleUsed { get; set; }
        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonPropertyName("transactionFee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Transaction fee unit
        /// </summary>
        [JsonPropertyName("transactionFeeUnit")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Execution price
        /// </summary>
        [JsonPropertyName("executionPrice")]
        public decimal ExecutionPrice { get; set; }
        /// <summary>
        /// Execution type
        /// </summary>
        [JsonPropertyName("executionType")]
        public AutoInvestExecutionType ExecutionType { get; set; }
        /// <summary>
        /// Subscription cycle
        /// </summary>
        [JsonPropertyName("subscriptionCycle")]
        public string SubscriptionCycle { get; set; } = string.Empty;
    }


}
