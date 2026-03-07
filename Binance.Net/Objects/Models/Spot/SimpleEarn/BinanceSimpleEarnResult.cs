namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn result
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnResult
    {
        /// <summary>
        /// ["<c>success</c>"] Whether the request succeeded.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}

