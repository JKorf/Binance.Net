namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// SOL staking quota
    /// </summary>
    [SerializationModel]
    public record BinanceSolStakingQuota
    {
        /// <summary>
        /// ["<c>leftStakingPersonalQuota</c>"] Staking quota left
        /// </summary>
        [JsonPropertyName("leftStakingPersonalQuota")]
        public decimal LeftStakingPersonalQuota { get; set; }
        /// <summary>
        /// ["<c>leftRedemptionPersonalQuota</c>"] Redemption quota left
        /// </summary>
        [JsonPropertyName("leftRedemptionPersonalQuota")]
        public decimal LeftRedemptionPersonalQuota { get; set; }
        /// <summary>
        /// ["<c>minStakeAmount</c>"] Min staking amount
        /// </summary>
        [JsonPropertyName("minStakeAmount")]
        public decimal MinStakeAmount { get; set; }
        /// <summary>
        /// ["<c>minRedeemAmount</c>"] Minimum redeem amount.
        /// </summary>
        [JsonPropertyName("minRedeemAmount")]
        public decimal MinRedeemAmount { get; set; }
        /// <summary>
        /// ["<c>redeemPeriod</c>"] Redeem period
        /// </summary>
        [JsonPropertyName("redeemPeriod")]
        public int RedeemPeriod { get; set; }
        /// <summary>
        /// ["<c>stakeable</c>"] Is stakeable
        /// </summary>
        [JsonPropertyName("stakeable")]
        public bool Stakeable { get; set; }
        /// <summary>
        /// ["<c>redeemable</c>"] Is redeemable
        /// </summary>
        [JsonPropertyName("redeemable")]
        public bool Redeemable { get; set; }
        /// <summary>
        /// ["<c>soldOut</c>"] Sold out
        /// </summary>
        [JsonPropertyName("soldOut")]
        public bool SoldOut { get; set; }
        /// <summary>
        /// ["<c>commissionFee</c>"] Commission fee
        /// </summary>
        [JsonPropertyName("commissionFee")]
        public decimal CommissionFee { get; set; }
        /// <summary>
        /// ["<c>nextEpochTime</c>"] Next time
        /// </summary>
        [JsonPropertyName("nextEpochTime")]
        public DateTime NextEpochTime { get; set; }
        /// <summary>
        /// ["<c>calculating</c>"] Calculating
        /// </summary>
        [JsonPropertyName("calculating")]
        public bool Calculating { get; set; }
    }
}

