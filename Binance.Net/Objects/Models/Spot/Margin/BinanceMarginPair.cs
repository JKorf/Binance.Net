namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin pair info
    /// </summary>
    [SerializationModel]
    public record BinanceMarginPair
    {
        /// <summary>
        /// ["<c>base</c>"] Base asset of the pair
        /// </summary>
        [JsonPropertyName("base")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote</c>"] Quote asset of the pair
        /// </summary>
        [JsonPropertyName("quote")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>isBuyAllowed</c>"] Is buying allowed
        /// </summary>
        [JsonPropertyName("isBuyAllowed")]
        public bool IsBuyAllowed { get; set; }
        /// <summary>
        /// ["<c>isSellAllowed</c>"] Is selling allowed
        /// </summary>
        [JsonPropertyName("isSellAllowed")]
        public bool IsSellAllowed { get; set; }
        /// <summary>
        /// ["<c>isMarginTrade</c>"] Is margin trading
        /// </summary>
        [JsonPropertyName("isMarginTrade")]
        public bool IsMarginTrade { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>delistTime</c>"] Time at which the symbol gets delisted
        /// </summary>
        [JsonPropertyName("delistTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DelistTime { get; set; }
    }
}

