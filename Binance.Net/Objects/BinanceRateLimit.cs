using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceRateLimit
    {
        public RateLimitInterval Interval { get; set; }
        [JsonProperty("rateLimitType")]
        public RateLimitType Type { get; set; }
        public int Limit { get; set; }
    }
}
