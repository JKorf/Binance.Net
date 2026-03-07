using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The result of placing a new OCO order
    /// </summary>
    [SerializationModel]
    public record BinanceOrderOcoList
    {
        /// <summary>
        /// ["<c>orderListId</c>"] The order list identifier.
        /// </summary>
        [JsonPropertyName("orderListId")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>contingencyType</c>"] The contingency type
        /// </summary>
        [JsonPropertyName("contingencyType")]
        public string ContingencyType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>listStatusType</c>"] The order list status
        /// </summary>
        [JsonPropertyName("listStatusType")]
        public ListStatusType ListStatusType { get; set; }
        /// <summary>
        /// ["<c>listOrderStatus</c>"] The order status
        /// </summary>
        [JsonPropertyName("listOrderStatus")]
        public ListOrderStatus ListOrderStatus { get; set; }
        /// <summary>
        /// ["<c>listClientOrderId</c>"] The client id of the order list
        /// </summary>
        [JsonPropertyName("listClientOrderId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ListClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transactionTime</c>"] The transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("transactionTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] The symbol of the order list
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orders</c>"] The order in this list
        /// </summary>
        [JsonPropertyName("orders")]
        public BinanceOrderId[] Orders { get; set; } = Array.Empty<BinanceOrderId>();
        /// <summary>
        /// ["<c>orderReports</c>"] The order details
        /// </summary>
        [JsonPropertyName("orderReports")]
        public BinancePlacedOcoOrder[] OrderReports { get; set; } = Array.Empty<BinancePlacedOcoOrder>();
    }

    /// <summary>
    /// Order reference
    /// </summary>
    public record BinanceOrderId
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol of the order
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] The order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] The client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientOrderId { get; set; } = string.Empty;
    }

    /// <summary>
    /// The result of placing a new order
    /// </summary>
    public record BinancePlacedOcoOrder : BinanceOrderBase
    {
    }
}

