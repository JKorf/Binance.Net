namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn result
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnResult
    {
        /// <summary>
        /// Result
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
