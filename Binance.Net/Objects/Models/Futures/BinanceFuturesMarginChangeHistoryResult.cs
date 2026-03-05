using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Result of the margin change history request
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesMarginChangeHistoryResult
    {
        /// <summary>
        /// The requested margin change quantity.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The margin asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string? Asset { get; set; }
        /// <summary>
        /// The symbol the margin change applies to.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        /// <summary>
        /// Delta type
        /// </summary>
        [JsonPropertyName("deltaType")]
        public string? DeltaType { get; set; }
        /// <summary>
        /// The margin change request time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The margin change direction.
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
