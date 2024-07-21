namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking position info
    /// </summary>
    public record BinanceStakingPosition
    {
        /// <summary>
        /// Position id
        /// </summary>
        public string? PositionId { get; set; }
        /// <summary>
        /// Project id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// Locked asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Locked quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Purchase time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? PurchaseTime { get; set; }
        /// <summary>
        /// Lock period in days
        /// </summary>
        public int? Duration { get; set; }
        /// <summary>
        /// Accrue days
        /// </summary>
        public int? AccrualDays { get; set; }
        /// <summary>
        /// Reward asset
        /// </summary>
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Apy
        /// </summary>
        public decimal Apy { get; set; }
        /// <summary>
        /// Earned quantity
        /// </summary>
        [JsonPropertyName("rewardAmt")]
        public decimal RewardQuantity { get; set; }
        /// <summary>
        /// Rewards asset of extra staking type
        /// </summary>
        public string? ExtraRewardAsset { get; set; }
        /// <summary>
        /// Extra rewards Apy
        /// </summary>
        public decimal? ExtraRewardApy { get; set; }
        /// <summary>
        /// Rewards of extra staking type, distribute when order expires
        /// </summary>
        [JsonPropertyName("estExtraRewardAmt")]
        public decimal? EstimatedExtraRewardQuantity { get; set; }
        /// <summary>
        /// Next estimated interest payment
        /// </summary>
        public decimal NextInterestPay { get; set; }
        /// <summary>
        /// Next interest payment date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextInterestPayDate { get; set; }
        /// <summary>
        /// Interest cycle
        /// </summary>
        public decimal PayInterestPeriod { get; set; }
        /// <summary>
        /// Early redemption amount
        /// </summary>
        [JsonPropertyName("redeemAmountEarly")]
        public decimal? RedeemQuantityEarly { get; set; }
        /// <summary>
        /// Interest accrual end date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? InterestEndDate { get; set; }
        /// <summary>
        /// Redemption arrival time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliverDate { get; set; }
        /// <summary>
        /// Redemption interval
        /// </summary>
        public decimal? RedeemPeriod { get; set; }
        /// <summary>
        /// Quantity under redemption
        /// </summary>
        [JsonPropertyName("redeemingAmt")]
        public decimal RedeemQuantity { get; set; }
        /// <summary>
        /// Arrival time of partial redemption amount of order
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? PartialQuantityDeliverDate { get; set; }
        /// <summary>
        /// When it is true, early redemption can be operated 
        /// </summary>
        public bool CanRedeemEarly { get; set; }
        /// <summary>
        /// When it is true, auto staking can be operated
        /// </summary>
        public bool Renewable { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        public string? Type { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        public string? Status { get; set; } = string.Empty;
    }
}
