using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Convert quote result
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesQuoteResult
    {
        /// <summary>
        /// The order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The order creation time.
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("orderStatus")]
        public ConvertOrderStatus Status { get; set; }
    }


}
