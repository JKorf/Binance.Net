namespace Binance.Net.Objects.Models.Spot.IsolatedMargin
{
    /// <summary>
    /// Isolated margin symbol info
    /// </summary>
    [SerializationModel]
    public record BinanceIsolatedMarginSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>base</c>"] Base asset
        /// </summary>
        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quote")]
        public string Quote { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isMarginTrade</c>"] Margin trade
        /// </summary>
        [JsonPropertyName("isMarginTrade")]
        public bool IsMarginTrade { get; set; }
        /// <summary>
        /// ["<c>isBuyAllowed</c>"] Is buy allowed
        /// </summary>
        [JsonPropertyName("isBuyAllowed")]
        public bool IsBuyAllowed { get; set; }
        /// <summary>
        /// ["<c>isSellAllowed</c>"] Is sell allowed
        /// </summary>
        [JsonPropertyName("isSellAllowed")]
        public bool IsSellAllowed { get; set; }
        /// <summary>
        /// ["<c>delistTime</c>"] Time at which the symbol gets delisted
        /// </summary>
        [JsonPropertyName("delistTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DelistTime { get; set; }
    }
}

