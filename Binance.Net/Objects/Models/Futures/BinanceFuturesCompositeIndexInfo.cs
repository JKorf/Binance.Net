namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Index info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCompositeIndexInfo
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] The data timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>component</c>"] The component asset.
        /// </summary>
        [JsonPropertyName("component")]
        public string Component { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>baseAssetList</c>"] Base asset list
        /// </summary>
        [JsonPropertyName("baseAssetList")]
        public BinanceFuturesCompositeIndexInfoAsset[] BaseAssets { get; set; } = Array.Empty<BinanceFuturesCompositeIndexInfoAsset>();
    }

    /// <summary>
    /// Composite index asset
    /// </summary>
    public record BinanceFuturesCompositeIndexInfoAsset
    {
        /// <summary>
        /// ["<c>baseAsset</c>"] Base asset name
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteAsset</c>"] Quote asset name
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>weightInQuantity</c>"] Weight in quantity
        /// </summary>
        [JsonPropertyName("weightInQuantity")]
        public decimal WeightInQuantity { get; set; }
        /// <summary>
        /// ["<c>weightInPercentage</c>"] Weight in percentage
        /// </summary>
        [JsonPropertyName("weightInPercentage")]
        public decimal WeightInPercentage { get; set; }
    }
}

