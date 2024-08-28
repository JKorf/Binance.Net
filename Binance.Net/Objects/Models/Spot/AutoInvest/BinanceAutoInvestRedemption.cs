using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Redemption info
    /// </summary>
    public record BinanceAutoInvestRedemption
    {
        /// <summary>
        /// Index id
        /// </summary>
        [JsonPropertyName("indexId")]
        public long IndexId { get; set; }
        /// <summary>
        /// Index name
        /// </summary>
        [JsonPropertyName("indexName")]
        public string IndexName { get; set; } = string.Empty;
        /// <summary>
        /// Redemption id
        /// </summary>
        [JsonPropertyName("redemptionId")]
        public long RedemptionId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestRedemptionStatus Status { get; set; }
        /// <summary>
        /// Asset
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
