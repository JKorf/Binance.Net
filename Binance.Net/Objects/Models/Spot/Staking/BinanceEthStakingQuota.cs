namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Eth staking quota
    /// </summary>
    [SerializationModel]
    public record BinanceEthStakingQuota
    {
        /// <summary>
        /// Remaining staking quota.
        /// </summary>
        [JsonPropertyName("leftStakingPersonalQuota")]
        public decimal LeftStakingPersonalQuota { get; set; }
        /// <summary>
        /// Remaining redemption quota.
        /// </summary>
        [JsonPropertyName("leftRedemptionPersonalQuota")]
        public decimal LeftRedemptionPersonalQuota { get; set; }
    }
}
