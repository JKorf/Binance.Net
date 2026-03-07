namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Locked product position info
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnLockedPosition
    {
        /// <summary>
        /// ["<c>asset</c>"] Product asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionId</c>"] Position id
        /// </summary>
        [JsonPropertyName("positionId"), JsonConverter(typeof(NumberStringConverter))]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>parentPositionId</c>"] Parent position identifier.
        /// </summary>
        [JsonPropertyName("parentPositionId"), JsonConverter(typeof(NumberStringConverter))]
        public string ParentPositionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>projectId</c>"] Project id
        /// </summary>
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>purchaseTime</c>"] Purchase time
        /// </summary>
        [JsonPropertyName("purchaseTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? PurchaseTime { get; set; }
        /// <summary>
        /// ["<c>duration</c>"] Duration in days
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
        /// <summary>
        /// ["<c>accrualDays</c>"] Accrual days
        /// </summary>
        [JsonPropertyName("accrualDays")]
        public int AccrualDays { get; set; }
        /// <summary>
        /// ["<c>rewardAsset</c>"] Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>APY</c>"] APY
        /// </summary>
        [JsonPropertyName("APY")]
        public decimal APY { get; set; }
        /// <summary>
        /// ["<c>isRenewable</c>"] Is renewable
        /// </summary>
        [JsonPropertyName("isRenewable")]
        public bool IsRenewable { get; set; }
        /// <summary>
        /// ["<c>isAutoRenew</c>"] Is auto renew enabled
        /// </summary>
        [JsonPropertyName("isAutoRenew")]
        public bool IsAutoRenew { get; set; }
        /// <summary>
        /// ["<c>redeemDate</c>"] Redeem date
        /// </summary>
        [JsonPropertyName("redeemDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? RedeemDate { get; set; }
        /// <summary>
        /// ["<c>rewardAmt</c>"] Reward quantity
        /// </summary>
        [JsonPropertyName("rewardAmt")]
        public decimal RewardQuantity { get; set; }
        /// <summary>
        /// ["<c>extraRewardAsset</c>"] Extra reward asset
        /// </summary>
        [JsonPropertyName("extraRewardAsset")]
        public string ExtraRewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>extraRewardAPR</c>"] Extra reward APR
        /// </summary>
        [JsonPropertyName("extraRewardAPR")]
        public decimal ExtraRewardApr { get; set; }
        /// <summary>
        /// ["<c>estExtraRewardAmt</c>"] Estimated extra reward quantity
        /// </summary>
        [JsonPropertyName("estExtraRewardAmt")]
        public decimal EstimatedExtraRewardQuantity { get; set; }

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
        /// ["<c>totalBoostRewardAmt</c>"] Total boost reward quantity
        /// </summary>
        [JsonPropertyName("totalBoostRewardAmt")]
        public decimal? TotalBoostRewardQuantity { get; set; }
        /// <summary>
        /// ["<c>nextPay</c>"] Estimated quantity of next payment
        /// </summary>
        [JsonPropertyName("nextPay")]
        public decimal? EstimatedNextPayQuantity { get; set; }
        /// <summary>
        /// ["<c>nextPayDate</c>"] Next pay time
        /// </summary>
        [JsonPropertyName("nextPayDate")]
        public DateTime? NextPayTime { get; set; }
        /// <summary>
        /// ["<c>payPeriod</c>"] Payment cycle
        /// </summary>
        [JsonPropertyName("payPeriod")]
        public string? PaymentPeriod { get; set; }
        /// <summary>
        /// ["<c>redeemAmountEarly</c>"] Early redemption quantity
        /// </summary>
        [JsonPropertyName("redeemAmountEarly")]
        public decimal? EarlyRedemptionQuantity { get; set; }
        /// <summary>
        /// ["<c>rewardsEndDate</c>"] Rewards accrual end time
        /// </summary>
        [JsonPropertyName("rewardsEndDate")]
        public DateTime? RewardsEndTime { get; set; }
        /// <summary>
        /// ["<c>deliverDate</c>"] Redemption arrival time
        /// </summary>
        [JsonPropertyName("deliverDate")]
        public DateTime? DeliverTime { get; set; }
        /// <summary>
        /// ["<c>redeemPeriod</c>"] Redeem period
        /// </summary>
        [JsonPropertyName("redeemPeriod")]
        public string? RedeemPeriod { get; set; }
        /// <summary>
        /// ["<c>redeemingAmt</c>"] Quantity under redemption
        /// </summary>
        [JsonPropertyName("redeemingAmt")]
        public decimal? RedemptionQuantity { get; set; }
        /// <summary>
        /// ["<c>redeemTo</c>"] Redeem target account
        /// </summary>
        [JsonPropertyName("redeemTo")]
        public string? RedeemTo { get; set; }
        /// <summary>
        /// ["<c>partialAmtDeliverDate</c>"] Arrival time of partial redemption amount of order
        /// </summary>
        [JsonPropertyName("partialAmtDeliverDate")]
        public DateTime? PartialRedemptionDeliverTime { get; set; }
        /// <summary>
        /// ["<c>canRedeemEarly</c>"] Can redeem early
        /// </summary>
        [JsonPropertyName("canRedeemEarly")]
        public bool? CanRedeemEarly { get; set; }
        /// <summary>
        /// ["<c>canFastRedemption</c>"] Can fast redeem
        /// </summary>
        [JsonPropertyName("canFastRedemption")]
        public bool? CanFastRedeem { get; set; }
        /// <summary>
        /// ["<c>autoSubscribe</c>"] Auto subscribe is enabled
        /// </summary>
        [JsonPropertyName("autoSubscribe")]
        public bool? AutoSubscribe { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Auto subscribe or normal
        /// </summary>
        [JsonPropertyName("type")]
        public string? OrderType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        /// <summary>
        /// ["<c>canReStake</c>"] Can restake
        /// </summary>
        [JsonPropertyName("canReStake")]
        public bool? CanRestake { get; set; }
    }
}

