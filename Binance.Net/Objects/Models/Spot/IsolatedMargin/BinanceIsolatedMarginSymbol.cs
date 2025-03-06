namespace Binance.Net.Objects.Models.Spot.IsolatedMargin
{
    /// <summary>
    /// Isolated margin symbol info
    /// </summary>
    [SerializationModel]
    public record BinanceIsolatedMarginSymbol
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quote")]
        public string Quote { get; set; } = string.Empty;
        /// <summary>
        /// Margin trade
        /// </summary>
        [JsonPropertyName("isMarginTrade")]
        public bool IsMarginTrade { get; set; }
        /// <summary>
        /// Is buy allowed
        /// </summary>
        [JsonPropertyName("isBuyAllowed")]
        public bool IsBuyAllowed { get; set; }
        /// <summary>
        /// Is sell allowed
        /// </summary>
        [JsonPropertyName("isSellAllowed")]
        public bool IsSellAllowed { get; set; }
        /// <summary>
        /// Time at which the symbol gets delisted
        /// </summary>
        [JsonPropertyName("delistTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DelistTime { get; set; }
    }
}
