using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Forced liquidation info
    /// </summary>
    public record BinanceForcedLiquidation
    {
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// The executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal ExecutedQuantity { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Total quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        public OrderSide Side { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Time in force
        /// </summary>
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updatedTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Is isolated margin
        /// </summary>
        public bool IsIsolated { get; set; }
    }
}
