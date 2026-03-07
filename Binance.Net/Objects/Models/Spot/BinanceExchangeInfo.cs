namespace Binance.Net.Objects.Models.Spot
{

    /// <summary>
    /// Exchange info
    /// </summary>
    [SerializationModel]
    public record BinanceExchangeInfo
    {
        /// <summary>
        /// ["<c>timezone</c>"] The timezone the server uses
        /// </summary>
        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>serverTime</c>"] The current server time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// ["<c>rateLimits</c>"] The rate limits used
        /// </summary>
        [JsonPropertyName("rateLimits")]
        public BinanceRateLimit[] RateLimits { get; set; } = Array.Empty<BinanceRateLimit>();
        /// <summary>
        /// ["<c>symbols</c>"] All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public BinanceSymbol[] Symbols { get; set; } = Array.Empty<BinanceSymbol>();
        /// <summary>
        /// ["<c>exchangeFilters</c>"] Exchange-level filters.
        /// </summary>
        [JsonPropertyName("exchangeFilters")]
        public object[] ExchangeFilters { get; set; } = Array.Empty<object>();
        /// <summary>
        /// ["<c>sors</c>"] Smart order routing
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
        /// ["<c>baseAsset</c>"] The base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbols</c>"] The symbols used for SOR
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = [];
    }
}

