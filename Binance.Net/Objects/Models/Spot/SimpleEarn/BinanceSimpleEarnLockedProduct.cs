namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple earn locked product info
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnLockedProduct
    {
        /// <summary>
        /// ["<c>projectId</c>"] Project identifier.
        /// </summary>
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>detail</c>"] Details
        /// </summary>
        [JsonPropertyName("detail")]
        public BinanceSimpleEarnLockedProjectDetails Details { get; set; } = null!;
        /// <summary>
        /// ["<c>quota</c>"] Quota
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
        /// ["<c>asset</c>"] Product asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rewardAsset</c>"] Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>duration</c>"] Duration
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
        /// <summary>
        /// ["<c>renewable</c>"] Renewable
        /// </summary>
        [JsonPropertyName("renewable")]
        public bool Renewable { get; set; }
        /// <summary>
        /// ["<c>isSoldOut</c>"] Is sold out
        /// </summary>
        [JsonPropertyName("isSoldOut")]
        public bool IsSoldOut { get; set; }
        /// <summary>
        /// ["<c>apr</c>"] Apr
        /// </summary>
        [JsonPropertyName("apr")]
        public decimal Apr { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>subscriptionStartTime</c>"] Subscription start time
        /// </summary>
        [JsonPropertyName("subscriptionStartTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? SubscriptionStartTime { get; set; }
        /// <summary>
        /// ["<c>extraRewardAsset</c>"] Extra reward asset
        /// </summary>
        [JsonPropertyName("extraRewardAsset")]
        public string ExtraRewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>extraRewardAPR</c>"] Extra reward apr
        /// </summary>
        [JsonPropertyName("extraRewardAPR")]
        public decimal ExtraRewardApr { get; set; }

        /// <summary>
        /// ["<c>boostRewardAsset</c>"] Asset the boost reward is in
        /// </summary>
        [JsonPropertyName("boostRewardAsset")]
        public string? BoostRewardAsset { get; set; }
        /// <summary>
        /// ["<c>boostApr</c>"] Boost apr
        /// </summary>
        [JsonPropertyName("boostApr")]
        public decimal? BoostApr { get; set; }
        /// <summary>
        /// ["<c>boostEndTime</c>"] Boost end time
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
        /// ["<c>totalPersonalQuota</c>"] Total personal quota
        /// </summary>
        [JsonPropertyName("totalPersonalQuota")]
        public decimal TotalPersonalQuota { get; set; }
        /// <summary>
        /// ["<c>minimum</c>"] Minimum
        /// </summary>
        [JsonPropertyName("minimum")]
        public decimal Minimum { get; set; }
    }
}

