using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Algo orders
    /// </summary>
    public record BinanceAlgoOrders
    {
        /// <summary>
        /// Total items
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public IEnumerable<BinanceAlgoOrder> Orders { get; set; } = Array.Empty<BinanceAlgoOrder>();
    }

    /// <summary>
    /// Algo order info
    /// </summary>
    public record BinanceAlgoOrder
    {
        /// <summary>
        /// Algo id
        /// </summary>
        [JsonPropertyName("algoId")]
        public long AlgoId { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// Total quantity
        /// </summary>
        [JsonPropertyName("totalQty")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// Executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal ExecutedQuantity { get; set; }
        /// <summary>
        /// Executed amount
        /// </summary>
        [JsonPropertyName("executedAmt")]
        public decimal ExecutedAmount { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Client algo id
        /// </summary>
        [JsonPropertyName("clientAlgoId")]
        [JsonConverterCtor(typeof(ReplaceConverter), 
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string ClientAlgoId { get; set; } = string.Empty;
        /// <summary>
        /// Book time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("bookTime")]
        public DateTime BookTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("endTime")]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("algoStatus")]
        public string AlgoStatus { get; set; } = string.Empty;
        /// <summary>
        /// Algo type
        /// </summary>
        [JsonPropertyName("algoType")]
        public string AlgoType { get; set; } = string.Empty;
        /// <summary>
        /// Urgency
        /// </summary>
        [JsonPropertyName("urgency")]
        public OrderUrgency? Urgency { get; set; }
    }
}
