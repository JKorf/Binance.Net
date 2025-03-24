namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple earn locked product info
    /// </summary>
    public record BinanceSimpleEarnLockedProduct
    {
        /// <summary>
        /// Project id
        /// </summary>
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("detail")]
        public BinanceSimpleEarnLockedProjectDetails Details { get; set; } = null!;
        /// <summary>
        /// Quota
        /// </summary>
        [JsonPropertyName("quota")]
        public BinanceSimpleEarnLockedProjectQuota Quota { get; set; } = null!;
    }

    /// <summary>
    /// Simple earn locked project details
    /// </summary>
    public record BinanceSimpleEarnLockedProjectDetails
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Duration
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
        /// <summary>
        /// Renewable
        /// </summary>
        [JsonPropertyName("renewable")]
        public bool Renewable { get; set; }
        /// <summary>
        /// Is sold out
        /// </summary>
        [JsonPropertyName("isSoldOut")]
        public bool IsSoldOut { get; set; }
        /// <summary>
        /// Apr
        /// </summary>
        [JsonPropertyName("apr")]
        public decimal Apr { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Subscription start time
        /// </summary>
        [JsonPropertyName("subscriptionStartTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? SubscriptionStartTime { get; set; }
        /// <summary>
        /// Extra reward asset
        /// </summary>
        [JsonPropertyName("extraRewardAsset")]
        public string ExtraRewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Extra reward apr
        /// </summary>
        [JsonPropertyName("extraRewardAPR")]
        public decimal ExtraRewardApr { get; set; }

        /// <summary>
        /// Asset the boost reward is in
        /// </summary>
        [JsonPropertyName("boostRewardAsset")]
        public string? BoostRewardAsset { get; set; }
        /// <summary>
        /// Boost apr
        /// </summary>
        [JsonPropertyName("boostApr")]
        public decimal? BoostApr { get; set; }
        /// <summary>
        /// Boost end time
        /// </summary>
        [JsonPropertyName("boostEndTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? BoostEndTime { get; set; }
    }

    /// <summary>
    /// Simple earn locked project quota
    /// </summary>
    public record BinanceSimpleEarnLockedProjectQuota
    {
        /// <summary>
        /// Total personal quota
        /// </summary>
        [JsonPropertyName("totalPersonalQuota")]
        public decimal TotalPersonalQuota { get; set; }
        /// <summary>
        /// Minimum
        /// </summary>
        [JsonPropertyName("minimum")]
        public decimal Minimum { get; set; }
    }
}
