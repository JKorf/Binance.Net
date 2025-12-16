namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Liability update
    /// </summary>
    public record BinanceLiabilityUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Listen key
        /// </summary>
        [JsonIgnore]
        public string ListenKey { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Liability update type
        /// </summary>
        [JsonPropertyName("t")]
        public string UpdateType { get; set; } = string.Empty;
        /// <summary>
        /// Principle quantity
        /// </summary>
        [JsonPropertyName("p")]
        public decimal? Principle { get; set; }
        /// <summary>
        /// Interest quantity
        /// </summary>
        [JsonPropertyName("i")]
        public decimal? Interest { get; set; }
    }
}
