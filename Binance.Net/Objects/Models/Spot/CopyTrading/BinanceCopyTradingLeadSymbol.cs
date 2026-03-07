namespace Binance.Net.Objects.Models.Spot.CopyTrading
{
    /// <summary>
    /// Copy trading lead symbol
    /// </summary>
    [SerializationModel]
    public record BinanceCopyTradingLeadSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseAsset</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteAsset</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
    }
}

