namespace Binance.Net
{
    internal partial class BinanceSourceGenerationAggregator
    {
        public Binance.Net.Objects.Models.Futures.BinanceFuturesUsdtExchangeInfo? BinanceFuturesUsdtExchangeInfo;
        public Binance.Net.Objects.Models.Futures.BinanceFuturesCoinExchangeInfo? BinanceFuturesCoinExchangeInfo;
    }
}

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record BinanceFuturesExchangeInfo
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
        /// Filters
        /// </summary>
        [JsonPropertyName("exchangeFilters")]
        public IEnumerable<object> ExchangeFilters { get; set; } = Array.Empty<object>();
    }

    /// <summary>
    /// Exchange info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesUsdtExchangeInfo: BinanceFuturesExchangeInfo
    {
        /// <summary>
        /// All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public IEnumerable<BinanceFuturesUsdtSymbol> Symbols { get; set; } = Array.Empty<BinanceFuturesUsdtSymbol>();

        /// <summary>
        /// All assets
        /// </summary>
        [JsonPropertyName("assets")]
        public IEnumerable<BinanceFuturesUsdtAsset> Assets { get; set; } = Array.Empty<BinanceFuturesUsdtAsset>();
    }

    /// <summary>
    /// Exchange info
    /// </summary>
    public record BinanceFuturesCoinExchangeInfo : BinanceFuturesExchangeInfo
    {
        /// <summary>
        /// All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public IEnumerable<BinanceFuturesCoinSymbol> Symbols { get; set; } = Array.Empty<BinanceFuturesCoinSymbol>();
    }
}
