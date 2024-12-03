using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The result of placing a new OCO order
    /// </summary>
    public record BinanceOrderOcoList
    {
        /// <summary>
        /// The id of the order list
        /// </summary>
        [JsonPropertyName("orderListId")]
        public long Id { get; set; }
        /// <summary>
        /// The contingency type
        /// </summary>
        [JsonPropertyName("contingencyType")]
        public string ContingencyType { get; set; } = string.Empty;
        /// <summary>
        /// The order list status
        /// </summary>
        [JsonPropertyName("listStatusType")]
        public ListStatusType ListStatusType { get; set; }
        /// <summary>
        /// The order status
        /// </summary>
        [JsonPropertyName("listOrderStatus")]
        public ListOrderStatus ListOrderStatus { get; set; }
        /// <summary>
        /// The client id of the order list
        /// </summary>
        [JsonPropertyName("listClientOrderId")]
        [JsonConverterCtor<ReplaceConverter>(
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string ListClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("transactionTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The symbol of the order list
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The order in this list
        /// </summary>
        [JsonPropertyName("orders")]
        public IEnumerable<BinanceOrderId> Orders { get; set; } = Array.Empty<BinanceOrderId>();
        /// <summary>
        /// The order details
        /// </summary>
        [JsonPropertyName("orderReports")]
        public IEnumerable<BinancePlacedOcoOrder> OrderReports { get; set; } = Array.Empty<BinancePlacedOcoOrder>();
    }

    /// <summary>
    /// Order reference
    /// </summary>
    public record BinanceOrderId
    {
        /// <summary>
        /// The symbol of the order
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverterCtor<ReplaceConverter>(
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string ClientOrderId { get; set; } = string.Empty;
    }

    /// <summary>
    /// The result of placing a new order
    /// </summary>
    public record BinancePlacedOcoOrder: BinanceOrderBase
    {
    }
}
