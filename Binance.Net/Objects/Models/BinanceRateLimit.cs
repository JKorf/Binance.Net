using Binance.Net.Enums;

namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Rate limit info
    /// </summary>
    public record BinanceRateLimit
    {
        /// <summary>
        /// The interval the rate limit uses to count
        /// </summary>
        [JsonPropertyName("interval")]
        public RateLimitInterval Interval { get; set; }
        /// <summary>
        /// The type the rate limit applies to
        /// </summary>
        [JsonPropertyName("rateLimitType")]
        public RateLimitType Type { get; set; }
        /// <summary>
        /// The amount of calls the limit is
        /// </summary>
        [JsonPropertyName("intervalNum")]
        public int IntervalNumber { get; set; }
        /// <summary>
        /// The amount of calls the limit is
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }
}
