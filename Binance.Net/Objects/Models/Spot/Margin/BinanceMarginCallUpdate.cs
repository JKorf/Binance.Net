using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin call update
    /// </summary>
    public record BinanceMarginCallUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Listen key
        /// </summary>
        [JsonIgnore]
        public string ListenKey { get; set; } = string.Empty;
        /// <summary>
        /// Margin level
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? MarginLevel { get; set; }
        /// <summary>
        /// Margin call status
        /// </summary>
        [JsonPropertyName("s")]
        public MarginLevelStatus MarginCallStatus { get; set; }
    }
}
