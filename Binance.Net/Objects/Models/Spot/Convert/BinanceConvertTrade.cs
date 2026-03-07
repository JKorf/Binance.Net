using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert trade info
    /// </summary>
    [SerializationModel]
    public record BinanceConvertTrade
    {
        /// <summary>
        /// ["<c>quoteId</c>"] Quote id
        /// </summary>
        [JsonPropertyName("quoteId")]
        public string QuoteId { get; set; } = string.Empty;
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
        /// ["<c>fromAsset</c>"] Quote asset 
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromAmount</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("fromAmount")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>toAsset</c>"] Base asset
        /// </summary>
        [JsonPropertyName("toAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toAmount</c>"] Base quantity
        /// </summary>
        [JsonPropertyName("toAmount")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// ["<c>ratio</c>"] Price ratio
        /// </summary>
        [JsonPropertyName("ratio")]
        public decimal Ratio { get; set; }
        /// <summary>
        /// ["<c>inverseRatio</c>"] Inverse price ratio
        /// </summary>
        [JsonPropertyName("inverseRatio")]
        public decimal InverseRatio { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
    }
}

