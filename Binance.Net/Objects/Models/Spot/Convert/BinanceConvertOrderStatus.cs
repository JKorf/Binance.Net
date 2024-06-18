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
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("orderStatus")]
        public ConvertOrderStatus Status { get; set; }
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonProperty("fromAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("fromAmount")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonProperty("toAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonProperty("toAmount")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// Price ratio
        /// </summary>
        public decimal Ratio { get; set; }
        /// <summary>
        /// Inverse price ratio
        /// </summary>
        public decimal InverseRatio { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
    }
}
