namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Rebalance info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestRebalanceInfo
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
        /// ["<c>rebalanceId</c>"] The rebalance identifier.
        /// </summary>
        [JsonPropertyName("rebalanceId")]
        public long RebalanceId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rebalanceFee</c>"] Rebalance fee
        /// </summary>
        [JsonPropertyName("rebalanceFee")]
        public decimal RebalanceFee { get; set; }
        /// <summary>
        /// ["<c>rebalanceFeeUnit</c>"] Rebalance fee unit
        /// </summary>
        [JsonPropertyName("rebalanceFeeUnit")]
        public string RebalanceFeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transactionDetails</c>"] Transaction details
        /// </summary>
        [JsonPropertyName("transactionDetails")]
        public BinanceAutoInvestRebalanceDetails[] TransactionDetails { get; set; } = Array.Empty<BinanceAutoInvestRebalanceDetails>();
    }

    /// <summary>
    /// Rebalance transaction details.
    /// </summary>
    public record BinanceAutoInvestRebalanceDetails
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transactionDateTime</c>"] Transaction date time
        /// </summary>
        [JsonPropertyName("transactionDateTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// ["<c>rebalanceDirection</c>"] Rebalance direction
        /// </summary>
        [JsonPropertyName("rebalanceDirection")]
        public string RebalanceDirection { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rebalanceAmount</c>"] Rebalance quantity
        /// </summary>
        [JsonPropertyName("rebalanceAmount")]
        public decimal RebalanceQuantity { get; set; }
    }


}

