namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Redemption
    /// </summary>
    public record BinanceSimpleEarnRedemption
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Redeem id
        /// </summary>
        [JsonPropertyName("redeemId")]
        public long RedeemId { get; set; }
    }
}
