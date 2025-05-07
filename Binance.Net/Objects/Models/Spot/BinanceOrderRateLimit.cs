namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Rate limit info
    /// </summary>
    [SerializationModel]
    public record BinanceCurrentRateLimit : BinanceRateLimit
    {
        /// <summary>
        /// The current used amount
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
