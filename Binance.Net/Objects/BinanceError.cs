using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceError
    {
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
