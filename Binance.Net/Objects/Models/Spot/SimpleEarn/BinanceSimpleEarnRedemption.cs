namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Redemption
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnRedemption
    {
        /// <summary>
        /// Whether the request succeeded.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Redemption identifier.
        /// </summary>
        [JsonPropertyName("redeemId")]
        public long RedeemId { get; set; }
    }
}
