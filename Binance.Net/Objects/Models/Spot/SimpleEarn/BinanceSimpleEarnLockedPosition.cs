namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Locked product position info
    /// </summary>
    public record BinanceSimpleEarnLockedPosition
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("positionId"), JsonConverter(typeof(NumberStringConverter))]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// Project id
        /// </summary>
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Purchase time
        /// </summary>
        [JsonPropertyName("purchaseTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? PurchaseTime { get; set; }
        /// <summary>
        /// Duration in days
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
        /// <summary>
        /// Accrual days
        /// </summary>
        [JsonPropertyName("accrualDays")]
        public int AccrualDays { get; set; }
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// APY
        /// </summary>
        [JsonPropertyName("APY")]
        public decimal APY { get; set; }
        /// <summary>
        /// Is renewable
        /// </summary>
        [JsonPropertyName("isRenewable")]
        public bool IsRenewable { get; set; }
        /// <summary>
        /// Is auto renew enabled
        /// </summary>
        [JsonPropertyName("isAutoRenew")]
        public bool IsAutoRenew { get; set; }
        /// <summary>
        /// Redeem date
        /// </summary>
        [JsonPropertyName("redeemDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? RedeemDate { get; set; }
    }
}
