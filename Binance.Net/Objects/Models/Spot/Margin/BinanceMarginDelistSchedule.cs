namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Delist margin schedule
    /// </summary>
    [SerializationModel]
    public record BinanceMarginDelistSchedule
    {
        /// <summary>
        /// ["<c>delistTime</c>"] Delist time
        /// </summary>
        [JsonPropertyName("delistTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DelistTime { get; set; }
        /// <summary>
        /// ["<c>crossMarginAssets</c>"] Cross margin assets
        /// </summary>
        [JsonPropertyName("crossMarginAssets")]
        public string[] CrossMarginAssets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>isolatedMarginSymbols</c>"] Isolated margin symbols
        /// </summary>
        [JsonPropertyName("isolatedMarginSymbols")]
        public string[] IsolatedMarginSymbols { get; set; } = Array.Empty<string>();
    }
}

