using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Algo orders
    /// </summary>
    [SerializationModel]
    public record BinanceAlgoOrders
    {
        /// <summary>
        /// ["<c>total</c>"] Total items
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>orders</c>"] Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public BinanceAlgoOrder[] Orders { get; set; } = Array.Empty<BinanceAlgoOrder>();
    }

    /// <summary>
    /// Algo order info
    /// </summary>
    public record BinanceAlgoOrder
    {
        /// <summary>
        /// ["<c>algoId</c>"] Algo id
        /// </summary>
        [JsonPropertyName("algoId")]
        public long AlgoId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>totalQty</c>"] Total quantity
        /// </summary>
        [JsonPropertyName("totalQty")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] Executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal ExecutedQuantity { get; set; }
        /// <summary>
        /// ["<c>executedAmt</c>"] Executed amount
        /// </summary>
        [JsonPropertyName("executedAmt")]
        public decimal ExecutedAmount { get; set; }
        /// <summary>
        /// ["<c>avgPrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// ["<c>clientAlgoId</c>"] Client algo id
        /// </summary>
        [JsonPropertyName("clientAlgoId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientAlgoId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bookTime</c>"] Book time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("bookTime")]
        public DateTime BookTime { get; set; }
        /// <summary>
        /// ["<c>endTime</c>"] End time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("endTime")]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// ["<c>algoStatus</c>"] Status
        /// </summary>
        [JsonPropertyName("algoStatus")]
        public string AlgoStatus { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>algoType</c>"] Algo type
        /// </summary>
        [JsonPropertyName("algoType")]
        public string AlgoType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>urgency</c>"] Urgency
        /// </summary>
        [JsonPropertyName("urgency")]
        public OrderUrgency? Urgency { get; set; }
    }
}

