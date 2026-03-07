namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Index price constituents info
    /// </summary>
    public record BinanceConstituents
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>constituents</c>"] Constituents
        /// </summary>
        [JsonPropertyName("constituents")]
        public BinanceConstituent[] Constituents { get; set; } = [];
    }

    /// <summary>
    /// Constituent info
    /// </summary>
    public record BinanceConstituent
    {
        /// <summary>
        /// ["<c>exchange</c>"] Exchange
        /// </summary>
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>weight</c>"] Weight
        /// </summary>
        [JsonPropertyName("weight")]
        public decimal? Weight { get; set; }
    }
}

