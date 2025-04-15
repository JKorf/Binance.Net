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
        public string EffectiveMultiple { get; set; } = string.Empty;
        /// <summary>
        /// Initial risk ratio
        /// </summary>
        [JsonPropertyName("initialRiskRatio")]
        public string InitialRiskRatio { get; set; } = string.Empty;
        /// <summary>
        /// Liquidation risk ratio
        /// </summary>
        [JsonPropertyName("liquidationRiskRatio")]
        public string LiquidationRiskRatio { get; set; } = string.Empty;
        /// <summary>
        /// Base asset max borrowable
        /// </summary>
        [JsonPropertyName("baseAssetMaxBorrowable")]
        public string BaseAssetMaxBorrowable { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset max borrowable
        /// </summary>
        [JsonPropertyName("quoteAssetMaxBorrowable")]
        public string QuoteAssetMaxBorrowable { get; set; } = string.Empty;
    }
}
