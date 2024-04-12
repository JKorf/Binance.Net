using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The status of Binance
    /// </summary>
    public class BinanceSystemStatus
    {
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(SystemStatusConverter))]
        public SystemStatus Status { get; set; }
        /// <summary>
        /// Additional info
        /// </summary>
        [JsonProperty("msg")]
        public string? Message { get; set; }
    }
}
