namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn personal quota left
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnPersonalQuotaLeft
    {
        /// <summary>
        /// Remaining personal quota.
        /// </summary>
        [JsonPropertyName("leftPersonalQuota")]
        public decimal PersonalQuotaLeft { get; set; }
    }
}
