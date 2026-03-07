using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert Quote
    /// </summary>
    [SerializationModel]
    public record BinanceConvertResult
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>createTime</c>"] Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>orderStatus</c>"] Order status
        /// </summary>
        [JsonPropertyName("orderStatus")]
        public ConvertOrderStatus Status { get; set; }
    }
}

