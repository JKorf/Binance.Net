using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceAccountStatus
    {
        [JsonProperty("msg")]
        public string Message { get; set; }
        public bool Success { get; set; }
        [JsonProperty("objs")]
        public object[] Objects { get; set; }
    }
}
