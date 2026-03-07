namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Quantile estimation
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesQuantileEstimation
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>adlQuantile</c>"] Quantile
        /// </summary>
        [JsonPropertyName("adlQuantile")]
        public BinanceFuturesAdlQuantile? AdlQuantile { get; set; }
    }

    /// <summary>
    /// Quantile info
    /// </summary>
    public record BinanceFuturesAdlQuantile
    {
        /// <summary>
        /// ["<c>LONG</c>"] Long position
        /// </summary>
        [JsonPropertyName("LONG")]
        public int Long { get; set; }
        /// <summary>
        /// ["<c>SHORT</c>"] Short position
        /// </summary>
        [JsonPropertyName("SHORT")]
        public int Short { get; set; }
        /// <summary>
        /// ["<c>HEDGE</c>"] Hedge
        /// </summary>
        [JsonPropertyName("HEDGE")]
        public int Hedge { get; set; }
        /// <summary>
        /// ["<c>BOTH</c>"] Hedge
        /// </summary>
        [JsonPropertyName("BOTH")]
        public int Both { get; set; }
    }
}

