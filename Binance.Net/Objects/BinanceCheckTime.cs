using Newtonsoft.Json;

namespace BinanceAPI.Objects
{
    public class BinanceCheckTime
    {
        [JsonProperty("serverTime")]
        public long ServerTime { get; set; }
    }
}
