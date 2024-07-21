namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Eth staking quota
    /// </summary>
    public record BinanceEthStakingQuota
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
    }
}
