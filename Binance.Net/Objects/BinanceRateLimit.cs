using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceRateLimit
    {
        /// <summary>
        /// The interval the ratelimit uses to count
        /// </summary>
        public RateLimitInterval Interval { get; set; }
        /// <summary>
        /// The type the rate limit applies to
        /// </summary>
        [JsonProperty("rateLimitType")]
        public RateLimitType Type { get; set; }
        /// <summary>
        /// The amount of calls the limit is
        /// </summary>
        public int Limit { get; set; }
    }
}
