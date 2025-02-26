//namespace Binance.Net
//{
//    internal partial class BinanceSourceGenerationAggregator
//    {
//        public Binance.Net.Objects.Models.Spot.BinanceExchangeInfo? BinanceExchangeInfo;
//    }
//}

namespace Binance.Net.Objects.Models.Spot
{

    /// <summary>
    /// Exchange info
    /// </summary>
    [SerializationModel]
    public record BinanceExchangeInfo
    {
        /// <summary>
        /// The timezone the server uses
        /// </summary>
        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// The current server time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// The rate limits used
        /// </summary>
        [JsonPropertyName("rateLimits")]
        public IEnumerable<BinanceRateLimit> RateLimits { get; set; } = Array.Empty<BinanceRateLimit>();
        /// <summary>
        /// All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public IEnumerable<BinanceSymbol> Symbols { get; set; } = Array.Empty<BinanceSymbol>();
        /// <summary>
        /// Filters
        /// </summary>
        [JsonPropertyName("exchangeFilters")]
        public IEnumerable<object> ExchangeFilters { get; set; } = Array.Empty<object>();
    }
}
