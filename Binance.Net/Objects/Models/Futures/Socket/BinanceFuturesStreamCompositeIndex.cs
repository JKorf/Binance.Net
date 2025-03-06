namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Composite index info
    /// </summary>
    public record BinanceFuturesStreamCompositeIndex : BinanceStreamEvent
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }

        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("C")]
        public string BaseAsset { get; set; } = string.Empty;

        /// <summary>
        /// Composition
        /// </summary>
        [JsonPropertyName("c")]
        public BinanceFuturesStreamCompositeIndexAsset[] Composition { get; set; } = Array.Empty<BinanceFuturesStreamCompositeIndexAsset>();
    }

    /// <summary>
    /// Composite index asset info
    /// </summary>
    public record BinanceFuturesStreamCompositeIndexAsset
    {
        /// <summary>
        /// Base asset name
        /// </summary>
        [JsonPropertyName("b")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset name
        /// </summary>
        [JsonPropertyName("q")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Weight in quantity
        /// </summary>
        [JsonPropertyName("w")]
        public decimal WeightInQuantity { get; set; }
        /// <summary>
        /// Weight in percentage
        /// </summary>
        [JsonPropertyName("W")]
        public decimal WeightInPercentage { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IndexPrice { get; set; }
    }
}
