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
        /// The redemption identifier.
        /// </summary>
        [JsonPropertyName("redemptionId")]
        public long RedemptionId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestRedemptionStatus Status { get; set; }
        /// <summary>
        /// The redeemed asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Redemption date time
        /// </summary>
        [JsonPropertyName("redemptionDateTime")]
        public DateTime RedeemTime { get; set; }
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
    }


}
