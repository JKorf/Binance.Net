namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// SOL staking quota
    /// </summary>
    public record BinanceSolStakingQuota
    {
        /// <summary>
        /// Staking quota left
        /// </summary>
        [JsonPropertyName("leftStakingPersonalQuota")]
        public decimal LeftStakingPersonalQuota { get; set; }
        /// <summary>
        /// Redemption quota left
        /// </summary>
        [JsonPropertyName("leftRedemptionPersonalQuota")]
        public decimal LeftRedemptionPersonalQuota { get; set; }
        /// <summary>
        /// Min staking amount
        /// </summary>
        [JsonPropertyName("minStakeAmount")]
        public decimal MinStakeAmount { get; set; }
        /// <summary>
        /// Min redeem amount
        /// </summary>
        [JsonPropertyName("minRedeemAmount")]
        public decimal MinRedeemAmount { get; set; }
        /// <summary>
        /// Redeem period
        /// </summary>
        [JsonPropertyName("redeemPeriod")]
        public int RedeemPeriod { get; set; }
        /// <summary>
        /// Is stakeable
        /// </summary>
        [JsonPropertyName("stakeable")]
        public bool Stakeable { get; set; }
        /// <summary>
        /// Is redeemable
        /// </summary>
        [JsonPropertyName("redeemable")]
        public bool Redeemable { get; set; }
        /// <summary>
        /// Sold out
        /// </summary>
        [JsonPropertyName("soldOut")]
        public bool SoldOut { get; set; }
        /// <summary>
        /// Commission fee
        /// </summary>
        [JsonPropertyName("commissionFee")]
        public decimal CommissionFee { get; set; }
        /// <summary>
        /// Next time
        /// </summary>
        [JsonPropertyName("nextEpochTime")]
        public DateTime NextEpochTime { get; set; }
        /// <summary>
        /// Calculating
        /// </summary>
        [JsonPropertyName("calculating")]
        public bool Calculating { get; set; }
    }
}
