namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Delist margin schedule
    /// </summary>
    public class BinanceMarginDelistSchedule
    {
        /// <summary>
        /// Delist time
        /// </summary>
        [JsonProperty("delistTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DelistTime { get; set; }
        /// <summary>
        /// Cross margin assets
        /// </summary>
        public IEnumerable<string> CrossMarginAssets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Isolated margin symbols
        /// </summary>
        public IEnumerable<string> IsolatedMarginSymbols { get; set; } = Array.Empty<string>();
    }
}
