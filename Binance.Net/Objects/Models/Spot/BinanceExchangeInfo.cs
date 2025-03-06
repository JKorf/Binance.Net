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
        public BinanceRateLimit[] RateLimits { get; set; } = Array.Empty<BinanceRateLimit>();
        /// <summary>
        /// All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public BinanceSymbol[] Symbols { get; set; } = Array.Empty<BinanceSymbol>();
        /// <summary>
        /// Filters
        /// </summary>
        [JsonPropertyName("exchangeFilters")]
        public object[] ExchangeFilters { get; set; } = Array.Empty<object>();
    }
}
