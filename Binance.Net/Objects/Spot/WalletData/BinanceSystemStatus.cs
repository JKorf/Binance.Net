using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.WalletData
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
