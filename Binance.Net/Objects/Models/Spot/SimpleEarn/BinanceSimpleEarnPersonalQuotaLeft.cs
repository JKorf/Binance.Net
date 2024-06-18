namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn personal quota left
    /// </summary>
    public record BinanceSimpleEarnPersonalQuotaLeft
    {
        /// <summary>
        /// Personal quota left
        /// </summary>
        [JsonProperty("leftPersonalQuota")]
        public decimal PersonalQuotaLeft { get; set; }
    }
}
