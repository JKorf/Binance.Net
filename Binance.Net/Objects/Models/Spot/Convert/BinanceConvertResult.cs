using System;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert Quote
    /// </summary>
    public class BinanceConvertResult
    {
        /// <summary>
        /// Order id
        /// </summary>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("orderStatus")]
        public ConvertOrderStatus Status { get; set; }
    }
}
