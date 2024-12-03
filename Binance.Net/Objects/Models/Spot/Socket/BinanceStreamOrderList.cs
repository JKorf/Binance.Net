using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Order list info
    /// </summary>
    public record BinanceStreamOrderList: BinanceStreamEvent
    {
        /// <summary>
        /// The id of the order list
        /// </summary>
        [JsonPropertyName("g")]
        public long Id { get; set; }
        /// <summary>
        /// The contingency type
        /// </summary>
        [JsonPropertyName("c")]
        public string ContingencyType { get; set; } = string.Empty;
        /// <summary>
        /// The order list status
        /// </summary>
        [JsonPropertyName("l")]
        public ListStatusType ListStatusType { get; set; }
        /// <summary>
        /// The order status
        /// </summary>
        [JsonPropertyName("L")]
        public ListOrderStatus ListOrderStatus { get; set; }
        /// <summary>
        /// Rejection reason
        /// </summary>
        [JsonPropertyName("r")]
        public string? ListRejectReason { get; set; }
        /// <summary>
        /// The client id of the order list
        /// </summary>
        [JsonPropertyName("C")]
        [JsonConverterCtor<ReplaceConverter>(
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string ListClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The symbol of the order list
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The order in this list
        /// </summary>
        [JsonPropertyName("O")]
        public IEnumerable<BinanceStreamOrderId> Orders { get; set; } = Array.Empty<BinanceStreamOrderId>();
        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Order reference
    /// </summary>
    public record BinanceStreamOrderId
    {
        /// <summary>
        /// The symbol of the order
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonPropertyName("c")]
        [JsonConverterCtor<ReplaceConverter>(
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
