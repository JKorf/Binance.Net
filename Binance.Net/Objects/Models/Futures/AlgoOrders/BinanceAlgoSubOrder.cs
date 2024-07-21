using Binance.Net.Converters;
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
        public long AlgoId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
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
        /// Exceuted amount
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
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Book time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime BookTime { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        public OrderSide Side { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Sub id
        /// </summary>
        public long SubId { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        public string TimeInForce { get; set; } = string.Empty;
        /// <summary>
        /// Original quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal OriginalQuantity { get; set; }
    }
}
