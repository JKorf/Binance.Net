namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin available inventory data
    /// </summary>
    public record BinanceMarginAvailableInventory
    {
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("assets")]
        public Dictionary<string, string> Assets { get; set; } = new();
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
