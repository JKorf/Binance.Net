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
        /// ["<c>amount</c>"] The requested margin change quantity.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] The margin asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string? Asset { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the margin change applies to.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        /// <summary>
        /// ["<c>deltaType</c>"] Delta type
        /// </summary>
        [JsonPropertyName("deltaType")]
        public string? DeltaType { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The margin change request time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>type</c>"] The margin change direction.
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesMarginChangeDirectionType Type { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
    }

}

