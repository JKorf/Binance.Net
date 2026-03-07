using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Transactions
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestPlanTransactions
    {
        /// <summary>
        /// ["<c>total</c>"] The total number of transactions.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>list</c>"] The returned transactions.
        /// </summary>
        [JsonPropertyName("list")]
        public BinanceAutoInvestPlanTransaction[] List { get; set; } = Array.Empty<BinanceAutoInvestPlanTransaction>();
    }

    /// <summary>
    /// Plan transaction info
    /// </summary>
    public record BinanceAutoInvestPlanTransaction
    {
        /// <summary>
        /// ["<c>id</c>"] The transaction identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>targetAsset</c>"] Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>planType</c>"] Plan type
        /// </summary>
        [JsonPropertyName("planType")]
        public AutoInvestPlanType PlanType { get; set; }
        /// <summary>
        /// ["<c>planName</c>"] Plan name
        /// </summary>
        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>planId</c>"] The plan identifier.
        /// </summary>
        [JsonPropertyName("planId")]
        public long PlanId { get; set; }
        /// <summary>
        /// ["<c>transactionDateTime</c>"] Transaction time
        /// </summary>
        [JsonPropertyName("transactionDateTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// ["<c>transactionStatus</c>"] Transaction status
        /// </summary>
        [JsonPropertyName("transactionStatus")]
        public AutoInvestTransactionStatus TransactionStatus { get; set; }
        /// <summary>
        /// ["<c>failedType</c>"] Failed type
        /// </summary>
        [JsonPropertyName("failedType")]
        public string? FailedType { get; set; }
        /// <summary>
        /// ["<c>sourceAsset</c>"] Source asset
        /// </summary>
        [JsonPropertyName("sourceAsset")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sourceAssetAmount</c>"] Source asset quantity
        /// </summary>
        [JsonPropertyName("sourceAssetAmount")]
        public decimal SourceAssetQuantity { get; set; }
        /// <summary>
        /// ["<c>targetAssetAmount</c>"] Target asset quantity
        /// </summary>
        [JsonPropertyName("targetAssetAmount")]
        public decimal TargetAssetQuantity { get; set; }
        /// <summary>
        /// ["<c>sourceWallet</c>"] Source wallet
        /// </summary>
        [JsonPropertyName("sourceWallet")]
        public string SourceWallet { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>flexibleUsed</c>"] Flexible used
        /// </summary>
        [JsonPropertyName("flexibleUsed")]
        public bool FlexibleUsed { get; set; }
        /// <summary>
        /// ["<c>transactionFee</c>"] Transaction fee
        /// </summary>
        [JsonPropertyName("transactionFee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>transactionFeeUnit</c>"] Transaction fee unit
        /// </summary>
        [JsonPropertyName("transactionFeeUnit")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>executionPrice</c>"] Execution price
        /// </summary>
        [JsonPropertyName("executionPrice")]
        public decimal ExecutionPrice { get; set; }
        /// <summary>
        /// ["<c>executionType</c>"] Execution type
        /// </summary>
        [JsonPropertyName("executionType")]
        public AutoInvestExecutionType ExecutionType { get; set; }
        /// <summary>
        /// ["<c>subscriptionCycle</c>"] Subscription cycle
        /// </summary>
        [JsonPropertyName("subscriptionCycle")]
        public string SubscriptionCycle { get; set; } = string.Empty;
    }


}

