using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Redemption info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestRedemption
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
        /// ["<c>redemptionId</c>"] The redemption identifier.
        /// </summary>
        [JsonPropertyName("redemptionId")]
        public long RedemptionId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestRedemptionStatus Status { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] The redeemed asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>redemptionDateTime</c>"] Redemption date time
        /// </summary>
        [JsonPropertyName("redemptionDateTime")]
        public DateTime RedeemTime { get; set; }
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
    }


}

