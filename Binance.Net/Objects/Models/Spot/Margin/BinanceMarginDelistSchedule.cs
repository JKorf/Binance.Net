﻿namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Delist margin schedule
    /// </summary>
    public record BinanceMarginDelistSchedule
    {
        /// <summary>
        /// Delist time
        /// </summary>
        [JsonPropertyName("delistTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DelistTime { get; set; }
        /// <summary>
        /// Cross margin assets
        /// </summary>
        [JsonPropertyName("crossMarginAssets")]
        public IEnumerable<string> CrossMarginAssets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Isolated margin symbols
        /// </summary>
        [JsonPropertyName("isolatedMarginSymbols")]
        public IEnumerable<string> IsolatedMarginSymbols { get; set; } = Array.Empty<string>();
    }
}
