namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Eth staking quota
    /// </summary>
    [SerializationModel]
    public record BinanceEthStakingQuota
    {
        /// <summary>
        /// ["<c>leftStakingPersonalQuota</c>"] Remaining staking quota.
        /// </summary>
        [JsonPropertyName("leftStakingPersonalQuota")]
        public decimal LeftStakingPersonalQuota { get; set; }
        /// <summary>
        /// ["<c>leftRedemptionPersonalQuota</c>"] Remaining redemption quota.
        /// </summary>
        [JsonPropertyName("leftRedemptionPersonalQuota")]
        public decimal LeftRedemptionPersonalQuota { get; set; }
    }
}

