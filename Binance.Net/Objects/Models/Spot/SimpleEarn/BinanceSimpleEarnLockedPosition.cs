namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Locked product position info
    /// </summary>
    [SerializationModel]
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
        /// Parent Position id
        /// </summary>
        [JsonPropertyName("parentPositionId"), JsonConverter(typeof(NumberStringConverter))]
        public string ParentPositionId { get; set; } = string.Empty;
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
        /// <summary>
        /// Reward quantity
        /// </summary>
        [JsonPropertyName("rewardAmt")]
        public decimal RewardQuantity { get; set; }
        /// <summary>
        /// Extra reward asset
        /// </summary>
        [JsonPropertyName("extraRewardAsset")]
        public string ExtraRewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Extra reward APR
        /// </summary>
        [JsonPropertyName("extraRewardAPR")]
        public decimal ExtraRewardApr { get; set; }
        /// <summary>
        /// Estimated extra reward quantity
        /// </summary>
        [JsonPropertyName("estExtraRewardAmt")]
        public decimal EstimatedExtraRewardQuantity { get; set; }

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
        /// Total boost reward quantity
        /// </summary>
        [JsonPropertyName("totalBoostRewardAmt")]
        public decimal? TotalBoostRewardQuantity { get; set; }
        /// <summary>
        /// Estimated quantity of next payment
        /// </summary>
        [JsonPropertyName("nextPay")]
        public decimal? EstimatedNextPayQuantity { get; set; }
        /// <summary>
        /// Next pay time
        /// </summary>
        [JsonPropertyName("nextPayDate")]
        public DateTime? NextPayTime { get; set; }
        /// <summary>
        /// Payment cycle
        /// </summary>
        [JsonPropertyName("payPeriod")]
        public string? PaymentPeriod { get; set; }
        /// <summary>
        /// Early redemption quantity
        /// </summary>
        [JsonPropertyName("redeemAmountEarly")]
        public decimal? EarlyRedemptionQuantity { get; set; }
        /// <summary>
        /// Rewards accrual end time
        /// </summary>
        [JsonPropertyName("rewardsEndDate")]
        public DateTime? RewardsEndTime { get; set; }
        /// <summary>
        /// Redemption arrival time
        /// </summary>
        [JsonPropertyName("deliverDate")]
        public DateTime? DeliverTime { get; set; }
        /// <summary>
        /// Redeem period
        /// </summary>
        [JsonPropertyName("redeemPeriod")]
        public string? RedeemPeriod { get; set; }
        /// <summary>
        /// Quantity under redemption
        /// </summary>
        [JsonPropertyName("redeemingAmt")]
        public decimal? RedemptionQuantity { get; set; }
        /// <summary>
        /// Redeem target account
        /// </summary>
        [JsonPropertyName("redeemTo")]
        public string? RedeemTo { get; set; }
        /// <summary>
        /// Arrival time of partial redemption amount of order
        /// </summary>
        [JsonPropertyName("partialAmtDeliverDate")]
        public DateTime? PartialRedemptionDeliverTime { get; set; }
        /// <summary>
        /// Can redeem early
        /// </summary>
        [JsonPropertyName("canRedeemEarly")]
        public bool? CanRedeemEarly { get; set; }
        /// <summary>
        /// Can fast redeem
        /// </summary>
        [JsonPropertyName("canFastRedemption")]
        public bool? CanFastRedeem { get; set; }
        /// <summary>
        /// Auto subscribe is enabled
        /// </summary>
        [JsonPropertyName("autoSubscribe")]
        public bool? AutoSubscribe { get; set; }
        /// <summary>
        /// Auto subscribe or normal
        /// </summary>
        [JsonPropertyName("type")]
        public string? OrderType { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        /// <summary>
        /// Can restake
        /// </summary>
        [JsonPropertyName("canReStake")]
        public bool? CanRestake { get; set; }
    }
}
