using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Sub order list
    /// </summary>
    [SerializationModel]
    public record BinanceAlgoSubOrderList
    {
        /// <summary>
        /// ["<c>total</c>"] Amount of sub orders
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
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
        /// ["<c>subOrders</c>"] Sub orders
        /// </summary>
        [JsonPropertyName("subOrders")]
        public BinanceAlgoSubOrder[] SubOrders { get; set; } = Array.Empty<BinanceAlgoSubOrder>();
    }

    /// <summary>
    /// Algo sub order info
    /// </summary>
    public record BinanceAlgoSubOrder
    {
        /// <summary>
        /// ["<c>algoId</c>"] Algo id
        /// </summary>
        [JsonPropertyName("algoId")]
        public long AlgoId { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>orderStatus</c>"] Order status
        /// </summary>
        [JsonPropertyName("orderStatus")]
        public OrderStatus Status { get; set; }
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
        /// ["<c>feeAmt</c>"] Fee amount
        /// </summary>
        [JsonPropertyName("feeAmt")]
        public decimal FeeAmount { get; set; }
        /// <summary>
        /// ["<c>feeAsset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bookTime</c>"] Book time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("bookTime")]
        public DateTime BookTime { get; set; }
        /// <summary>
        /// ["<c>avgPrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
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
        /// ["<c>subId</c>"] Sub id
        /// </summary>
        [JsonPropertyName("subId")]
        public long SubId { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public string TimeInForce { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>origQty</c>"] Original quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal OriginalQuantity { get; set; }
    }
}

