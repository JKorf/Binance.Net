namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn locked product preview
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnLockedPreview
    {
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Extra reward asset
        /// </summary>
        [JsonPropertyName("extraRewardAsset")]
        public string ExtraRewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Total reward quantity
        /// </summary>
        [JsonPropertyName("totalRewardAmt")]
        public decimal TotalRewardQuantity { get; set; }
        /// <summary>
        /// Estimated total extra reward quantity
        /// </summary>
        [JsonPropertyName("estTotalExtraRewardAmt")]
        public decimal EstimatedTotalExtraRewardQuantity { get; set; }
        /// <summary>
        /// Next pay
        /// </summary>
        [JsonPropertyName("nextPay")]
        public decimal NextPay { get; set; }
        /// <summary>
        /// Next pay date
        /// </summary>
        [JsonPropertyName("nextPayDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextPayDate { get; set; }
        /// <summary>
        /// Value date
        /// </summary>
        [JsonPropertyName("valueDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ValueDate { get; set; }
        /// <summary>
        /// Rewards end date
        /// </summary>
        [JsonPropertyName("rewardsEndDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime RewardsEndDate { get; set; }
        /// <summary>
        /// Deliver date
        /// </summary>
        [JsonPropertyName("deliverDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliverDate { get; set; }
        /// <summary>
        /// Next subscription date
        /// </summary>
        [JsonPropertyName("nextSubscriptionDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextSubscriptionDate { get; set; }
    }
}
