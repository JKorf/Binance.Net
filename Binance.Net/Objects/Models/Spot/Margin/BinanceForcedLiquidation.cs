using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Forced liquidation info
    /// </summary>
    [SerializationModel]
    public record BinanceForcedLiquidation
    {
        /// <summary>
        /// ["<c>avgPrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] The executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal ExecutedQuantity { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Total quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>updatedTime</c>"] Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updatedTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>isIsolated</c>"] Is isolated margin
        /// </summary>
        [JsonPropertyName("isIsolated")]
        public bool IsIsolated { get; set; }
    }
}

