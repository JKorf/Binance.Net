using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceWithdrawalPlaced
    {
        public bool Success { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
