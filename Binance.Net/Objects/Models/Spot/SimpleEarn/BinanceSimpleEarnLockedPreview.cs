namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn locked product preview
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnLockedPreview
    {
        /// <summary>
        /// ["<c>rewardAsset</c>"] Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>extraRewardAsset</c>"] Extra reward asset
        /// </summary>
        [JsonPropertyName("extraRewardAsset")]
        public string ExtraRewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalRewardAmt</c>"] Total reward quantity
        /// </summary>
        [JsonPropertyName("totalRewardAmt")]
        public decimal TotalRewardQuantity { get; set; }
        /// <summary>
        /// ["<c>estTotalExtraRewardAmt</c>"] Estimated total extra reward quantity
        /// </summary>
        [JsonPropertyName("estTotalExtraRewardAmt")]
        public decimal EstimatedTotalExtraRewardQuantity { get; set; }
        /// <summary>
        /// ["<c>nextPay</c>"] Next pay amount.
        /// </summary>
        [JsonPropertyName("nextPay")]
        public decimal NextPay { get; set; }
        /// <summary>
        /// ["<c>nextPayDate</c>"] Next pay date
        /// </summary>
        [JsonPropertyName("nextPayDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextPayDate { get; set; }
        /// <summary>
        /// ["<c>valueDate</c>"] Value date
        /// </summary>
        [JsonPropertyName("valueDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ValueDate { get; set; }
        /// <summary>
        /// ["<c>rewardsEndDate</c>"] Rewards end date
        /// </summary>
        [JsonPropertyName("rewardsEndDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime RewardsEndDate { get; set; }
        /// <summary>
        /// ["<c>deliverDate</c>"] Deliver date
        /// </summary>
        [JsonPropertyName("deliverDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliverDate { get; set; }
        /// <summary>
        /// ["<c>nextSubscriptionDate</c>"] Next subscription date
        /// </summary>
        [JsonPropertyName("nextSubscriptionDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextSubscriptionDate { get; set; }
        /// <summary>
        /// ["<c>boostRewardAsset</c>"] Asset the boost reward is in
        /// </summary>
        [JsonPropertyName("boostRewardAsset")]
        public string? BoostRewardAsset { get; set; }
        /// <summary>
        /// ["<c>estDailyRewardAmt</c>"] Estimated daily reward
        /// </summary>
        [JsonPropertyName("estDailyRewardAmt")]
        public decimal? EstimatedDailyRewardQuantity { get; set; }
    }
}

