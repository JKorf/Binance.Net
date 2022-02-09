using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Convert trade info
    /// </summary>
    public class BinanceConvertTrade
    {
        /// <summary>
        /// Quote id
        /// </summary>
        public string QuoteId { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order status
        /// </summary>
        public string OrderStatus { get; set; } = string.Empty;
        /// <summary>
        /// From asset
        /// </summary>
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// From quantity
        /// </summary>
        [JsonProperty("fromAmount")]
        public decimal FromQuantity { get; set; }
        /// <summary>
        /// To asset
        /// </summary>
        public string ToAsset { get; set; } = string.Empty;
        /// <summary>
        /// To quantity
        /// </summary>
        [JsonProperty("toAmount")]
        public decimal ToQuantity { get; set; }
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
