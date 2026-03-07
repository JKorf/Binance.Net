using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Order list info
    /// </summary>
    [SerializationModel]
    public record BinanceStreamOrderList : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>g</c>"] The order list identifier.
        /// </summary>
        [JsonPropertyName("g")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>c</c>"] The contingency type
        /// </summary>
        [JsonPropertyName("c")]
        public string ContingencyType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>l</c>"] The order list status
        /// </summary>
        [JsonPropertyName("l")]
        public ListStatusType ListStatusType { get; set; }
        /// <summary>
        /// ["<c>L</c>"] The order status
        /// </summary>
        [JsonPropertyName("L")]
        public ListOrderStatus ListOrderStatus { get; set; }
        /// <summary>
        /// ["<c>r</c>"] Rejection reason
        /// </summary>
        [JsonPropertyName("r")]
        public string? ListRejectReason { get; set; }
        /// <summary>
        /// ["<c>C</c>"] The client id of the order list
        /// </summary>
        [JsonPropertyName("C")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ListClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>T</c>"] The transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// ["<c>s</c>"] The symbol of the order list
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>O</c>"] The order in this list
        /// </summary>
        [JsonPropertyName("O")]
        public BinanceStreamOrderId[] Orders { get; set; } = Array.Empty<BinanceStreamOrderId>();
        /// <summary>
        /// API key this update was for.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Order reference
    /// </summary>
    public record BinanceStreamOrderId
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol of the order
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>i</c>"] The id of the order
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>c</c>"] The client order id
        /// </summary>
        [JsonPropertyName("c")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}

