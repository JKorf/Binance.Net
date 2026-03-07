using Binance.Net.Enums;

namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Rate limit info
    /// </summary>
    [SerializationModel]
    public record BinanceRateLimit
    {
        /// <summary>
        /// ["<c>interval</c>"] The interval the rate limit uses to count
        /// </summary>
        [JsonPropertyName("interval")]
        public RateLimitInterval Interval { get; set; }
        /// <summary>
        /// ["<c>rateLimitType</c>"] The type the rate limit applies to
        /// </summary>
        [JsonPropertyName("rateLimitType")]
        public RateLimitType Type { get; set; }
        /// <summary>
        /// ["<c>intervalNum</c>"] The amount of calls the limit is
        /// </summary>
        [JsonPropertyName("intervalNum")]
        public int IntervalNumber { get; set; }
        /// <summary>
        /// ["<c>limit</c>"] The amount of calls the limit is
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }
}

