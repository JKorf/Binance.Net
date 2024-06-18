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
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Quantile
        /// </summary>
        public BinanceFuturesAdlQuantile? AdlQuantile{ get; set; }
    }

    /// <summary>
    /// Quantile info
    /// </summary>
    public record BinanceFuturesAdlQuantile
    {
        /// <summary>
        /// Long position
        /// </summary>
        public int Long { get; set; }
        /// <summary>
        /// Short position
        /// </summary>
        public int Short { get; set; }
        /// <summary>
        /// Hedge
        /// </summary>
        public int Hedge { get; set; }
        /// <summary>
        /// Hedge
        /// </summary>
        public int Both { get; set; }
    }
}
