namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Rebalance info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestRebalanceInfo
    {
        /// <summary>
        /// The index identifier.
        /// </summary>
        [JsonPropertyName("indexId")]
        public long IndexId { get; set; }
        /// <summary>
        /// Index name
        /// </summary>
        [JsonPropertyName("indexName")]
        public string IndexName { get; set; } = string.Empty;
        /// <summary>
        /// The rebalance identifier.
        /// </summary>
        [JsonPropertyName("rebalanceId")]
        public long RebalanceId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Rebalance fee
        /// </summary>
        [JsonPropertyName("rebalanceFee")]
        public decimal RebalanceFee { get; set; }
        /// <summary>
        /// Rebalance fee unit
        /// </summary>
        [JsonPropertyName("rebalanceFeeUnit")]
        public string RebalanceFeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Transaction details
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
        /// The asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Transaction date time
        /// </summary>
        [JsonPropertyName("transactionDateTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// Rebalance direction
        /// </summary>
        [JsonPropertyName("rebalanceDirection")]
        public string RebalanceDirection { get; set; } = string.Empty;
        /// <summary>
        /// Rebalance quantity
        /// </summary>
        [JsonPropertyName("rebalanceAmount")]
        public decimal RebalanceQuantity { get; set; }
    }


}
