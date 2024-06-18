namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn locked product preview
    /// </summary>
    public record BinanceSimpleEarnLockedPreview
    {
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonProperty("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Extra reward asset
        /// </summary>
        [JsonProperty("extraRewardAsset")]
        public string ExtraRewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Total reward quantity
        /// </summary>
        [JsonProperty("totalRewardAmt")]
        public decimal TotalRewardQuantity { get; set; }
        /// <summary>
        /// Estimated total extra reward quantity
        /// </summary>
        [JsonProperty("estTotalExtraRewardAmt")]
        public decimal EstimatedTotalExtraRewardQuantity { get; set; }
        /// <summary>
        /// Next pay
        /// </summary>
        [JsonProperty("nextPay")]
        public decimal NextPay { get; set; }
        /// <summary>
        /// Next pay date
        /// </summary>
        [JsonProperty("nextPayDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextPayDate { get; set; }
        /// <summary>
        /// Value date
        /// </summary>
        [JsonProperty("valueDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ValueDate { get; set; }
        /// <summary>
        /// Rewards end date
        /// </summary>
        [JsonProperty("rewardsEndDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime RewardsEndDate { get; set; }
        /// <summary>
        /// Deliver date
        /// </summary>
        [JsonProperty("deliverDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliverDate { get; set; }
        /// <summary>
        /// Next subscription date
        /// </summary>
        [JsonProperty("nextSubscriptionDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextSubscriptionDate { get; set; }
    }
}
