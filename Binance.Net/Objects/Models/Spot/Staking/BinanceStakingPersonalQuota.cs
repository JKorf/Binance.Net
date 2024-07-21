namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Quota left
    /// </summary>
    public record BinanceStakingPersonalQuota
    {
        /// <summary>
        /// Quota left
        /// </summary>
        [JsonPropertyName("leftPersonalQuota")]
        public decimal PersonalQuotaLeft { get; set; }
    }
}
