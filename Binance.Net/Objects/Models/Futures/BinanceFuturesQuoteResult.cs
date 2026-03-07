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
        /// ["<c>orderId</c>"] The order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>createTime</c>"] The order creation time.
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>orderStatus</c>"] Order status
        /// </summary>
        [JsonPropertyName("orderStatus")]
        public ConvertOrderStatus Status { get; set; }
    }


}

