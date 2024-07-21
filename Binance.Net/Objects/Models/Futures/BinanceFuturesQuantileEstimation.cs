namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Quantile estimation
    /// </summary>
    public record BinanceFuturesQuantileEstimation
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Quantile
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
        /// Long position
        /// </summary>
        [JsonPropertyName("long")]
        public int Long { get; set; }
        /// <summary>
        /// Short position
        /// </summary>
        [JsonPropertyName("short")]
        public int Short { get; set; }
        /// <summary>
        /// Hedge
        /// </summary>
        [JsonPropertyName("hedge")]
        public int Hedge { get; set; }
        /// <summary>
        /// Hedge
        /// </summary>
        [JsonPropertyName("both")]
        public int Both { get; set; }
    }
}
