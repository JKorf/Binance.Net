namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Composite index info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamCompositeIndex : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>p</c>"] The price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }

        /// <summary>
        /// ["<c>C</c>"] The base asset.
        /// </summary>
        [JsonPropertyName("C")]
        public string BaseAsset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>c</c>"] Composition
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
        /// ["<c>b</c>"] Base asset name
        /// </summary>
        [JsonPropertyName("b")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>q</c>"] Quote asset name
        /// </summary>
        [JsonPropertyName("q")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>w</c>"] Weight in quantity
        /// </summary>
        [JsonPropertyName("w")]
        public decimal WeightInQuantity { get; set; }
        /// <summary>
        /// ["<c>W</c>"] Weight in percentage
        /// </summary>
        [JsonPropertyName("W")]
        public decimal WeightInPercentage { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Index price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IndexPrice { get; set; }
    }
}

