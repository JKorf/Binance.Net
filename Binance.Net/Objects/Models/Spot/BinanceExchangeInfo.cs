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
        /// <summary>
        /// Smart order routing
        /// </summary>
        [JsonPropertyName("sors")]
        public BinanceSor[]? SmartOrderRoutings { get; set; }
    }

    /// <summary>
    /// Smart order routing configuration
    /// </summary>
    public record BinanceSor
    {
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The symbols used for SOR
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = [];
    }
}
