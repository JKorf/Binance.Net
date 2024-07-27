namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin pair info
    /// </summary>
    public record BinanceMarginPair
    {
        /// <summary>
        /// Base asset of the pair
        /// </summary>
        [JsonPropertyName("base")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset of the pair
        /// </summary>
        [JsonPropertyName("quote")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Is buying allowed
        /// </summary>
        [JsonPropertyName("isBuyAllowed")]
        public bool IsBuyAllowed { get; set; }
        /// <summary>
        /// Is selling allowed
        /// </summary>
        [JsonPropertyName("isSellAllowed")]
        public bool IsSellAllowed { get; set; }
        /// <summary>
        /// Is margin trading
        /// </summary>
        [JsonPropertyName("isMarginTrade")]
        public bool IsMarginTrade { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Time at which the symbol gets delisted
        /// </summary>
        [JsonPropertyName("delistTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DelistTime { get; set; }
    }
}
