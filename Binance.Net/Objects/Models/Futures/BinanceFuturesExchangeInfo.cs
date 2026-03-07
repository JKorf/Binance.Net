namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record BinanceFuturesExchangeInfo
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
        /// ["<c>exchangeFilters</c>"] Filters
        /// </summary>
        [JsonPropertyName("exchangeFilters")]
        public object[] ExchangeFilters { get; set; } = Array.Empty<object>();
    }

    /// <summary>
    /// Exchange info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesUsdtExchangeInfo : BinanceFuturesExchangeInfo
    {
        /// <summary>
        /// ["<c>symbols</c>"] All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public BinanceFuturesUsdtSymbol[] Symbols { get; set; } = Array.Empty<BinanceFuturesUsdtSymbol>();

        /// <summary>
        /// ["<c>assets</c>"] All assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceFuturesUsdtAsset[] Assets { get; set; } = Array.Empty<BinanceFuturesUsdtAsset>();
    }

    /// <summary>
    /// Exchange info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinExchangeInfo : BinanceFuturesExchangeInfo
    {
        /// <summary>
        /// ["<c>symbols</c>"] All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public BinanceFuturesCoinSymbol[] Symbols { get; set; } = Array.Empty<BinanceFuturesCoinSymbol>();
    }
}

