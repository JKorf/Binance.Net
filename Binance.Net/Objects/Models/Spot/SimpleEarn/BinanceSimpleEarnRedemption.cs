namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Redemption
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnRedemption
    {
        /// <summary>
        /// ["<c>success</c>"] Whether the request succeeded.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>redeemId</c>"] Redemption identifier.
        /// </summary>
        [JsonPropertyName("redeemId")]
        public long RedeemId { get; set; }
    }
}

