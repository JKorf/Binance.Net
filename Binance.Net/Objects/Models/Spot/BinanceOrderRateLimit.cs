namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Rate limit info
    /// </summary>
    [SerializationModel]
    public record BinanceCurrentRateLimit : BinanceRateLimit
    {
        /// <summary>
        /// ["<c>count</c>"] The currently used amount.
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}

