using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures stream trading sessions update
    /// </summary>
    public record BinanceTradingSessionUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Session start time.
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Session end time.
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Session type
        /// </summary>
        [JsonPropertyName("S")]
        public SessionType Type { get; set; }
    }
}
