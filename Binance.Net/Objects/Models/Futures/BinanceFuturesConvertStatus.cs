using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Convert order status
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesConvertStatus
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>orderStatus</c>"] Order status
        /// </summary>
        [JsonPropertyName("orderStatus")]
        public ConvertOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>fromAsset</c>"] From asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromAmount</c>"] Quantity in the from asset
        /// </summary>
        [JsonPropertyName("fromAmount")]
        public decimal FromQuantity { get; set; }
        /// <summary>
        /// ["<c>toAsset</c>"] To asset
        /// </summary>
        [JsonPropertyName("toAsset")]
        public string ToAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toAmount</c>"] Quantity in the to asset
        /// </summary>
        [JsonPropertyName("toAmount")]
        public decimal ToQuantity { get; set; }
        /// <summary>
        /// ["<c>ratio</c>"] Ratio
        /// </summary>
        [JsonPropertyName("ratio")]
        public decimal Ratio { get; set; }
        /// <summary>
        /// ["<c>inverseRatio</c>"] Inverse ratio
        /// </summary>
        [JsonPropertyName("inverseRatio")]
        public decimal InverseRatio { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
    }


}

