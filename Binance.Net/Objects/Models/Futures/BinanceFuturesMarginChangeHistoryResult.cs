using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Result of the margin change history request
    /// </summary>
    public record BinanceFuturesMarginChangeHistoryResult
    {
        /// <summary>
        /// Request quantity of margin used
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Base asset used for margin
        /// </summary>
        [JsonPropertyName("asset")]
        public string? Asset { get; set; }
        /// <summary>
        /// Symbol margin is placed on
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        /// <summary>
        /// Delta type
        /// </summary>
        [JsonPropertyName("deltaType")]
        public string? DeltaType { get; set; }
        /// <summary>
        /// Time of the margin change request
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Direction of the margin change request
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesMarginChangeDirectionType Type { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
    }

}
