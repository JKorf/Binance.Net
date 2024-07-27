namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn result
    /// </summary>
    public record BinanceSimpleEarnResult
    {
        /// <summary>
        /// Result
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
