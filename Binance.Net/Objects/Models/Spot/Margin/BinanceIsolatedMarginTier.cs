namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Binance isolated margin tier data
    /// </summary>
    public record BinanceIsolatedMarginTier
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int Tier { get; set; }
        /// <summary>
        /// Effective multiple
        /// </summary>
        [JsonPropertyName("effectiveMultiple")]
        public decimal? EffectiveMultiple { get; set; } = null;
        /// <summary>
        /// Initial risk ratio
        /// </summary>
        [JsonPropertyName("initialRiskRatio")]
        public decimal? InitialRiskRatio { get; set; } = null;
        /// <summary>
        /// Liquidation risk ratio
        /// </summary>
        [JsonPropertyName("liquidationRiskRatio")]
        public decimal? LiquidationRiskRatio { get; set; } = null;
        /// <summary>
        /// Base asset max borrowable
        /// </summary>
        [JsonPropertyName("baseAssetMaxBorrowable")]
        public decimal? BaseAssetMaxBorrowable { get; set; } = null;
        /// <summary>
        /// Quote asset max borrowable
        /// </summary>
        [JsonPropertyName("quoteAssetMaxBorrowable")]
        public decimal? QuoteAssetMaxBorrowable { get; set; } = null;
    }
}
