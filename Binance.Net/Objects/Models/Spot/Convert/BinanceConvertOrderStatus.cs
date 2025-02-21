using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert trade info
    /// </summary>
    public record BinanceConvertOrderStatus
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(EnumConverter<ConvertOrderStatus>))]
        [JsonPropertyName("orderStatus")]
        public ConvertOrderStatus Status { get; set; }
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonPropertyName("fromAmount")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("toAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonPropertyName("toAmount")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// Price ratio
        /// </summary>
        [JsonPropertyName("ratio")]
        public decimal Ratio { get; set; }
        /// <summary>
        /// Inverse price ratio
        /// </summary>
        [JsonPropertyName("inverseRatio")]
        public decimal InverseRatio { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
    }
}
