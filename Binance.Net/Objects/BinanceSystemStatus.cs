using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceSystemStatus
    {
        [JsonConverter(typeof(SystemStatusConverter))]
        public SystemStatus Status { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
