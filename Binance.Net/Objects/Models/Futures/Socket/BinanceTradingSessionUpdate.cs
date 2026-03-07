using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures stream trading sessions update
    /// </summary>
    public record BinanceTradingSessionUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>t</c>"] Session start time.
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// ["<c>T</c>"] Session end time.
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// ["<c>S</c>"] Session type
        /// </summary>
        [JsonPropertyName("S")]
        public SessionType Type { get; set; }
    }
}

