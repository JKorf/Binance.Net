using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Sub order list
    /// </summary>
    public record BinanceAlgoSubOrderList
    {
        /// <summary>
        /// Amount of sub orders
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
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
        /// Sub orders
        /// </summary>
        [JsonPropertyName("subOrders")]
        public IEnumerable<BinanceAlgoSubOrder> SubOrders { get; set; } = Array.Empty<BinanceAlgoSubOrder>();
    }

    /// <summary>
    /// Algo sub order info
    /// </summary>
    public record BinanceAlgoSubOrder
    {
        /// <summary>
        /// Algo id
        /// </summary>
        [JsonPropertyName("algoId")]
        public long AlgoId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("orderStatus")]
        public OrderStatus Status { get; set; }
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
        /// Fee amount
        /// </summary>
        [JsonPropertyName("feeAmt")]
        public decimal FeeAmount { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Book time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("bookTime")]
        public DateTime BookTime { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Sub id
        /// </summary>
        [JsonPropertyName("subId")]
        public long SubId { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public string TimeInForce { get; set; } = string.Empty;
        /// <summary>
        /// Original quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal OriginalQuantity { get; set; }
    }
}
