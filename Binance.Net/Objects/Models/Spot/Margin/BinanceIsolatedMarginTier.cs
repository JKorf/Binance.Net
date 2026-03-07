namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Binance isolated margin tier data
    /// </summary>
    public record BinanceIsolatedMarginTier
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tier</c>"] Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int Tier { get; set; }
        /// <summary>
        /// ["<c>effectiveMultiple</c>"] Effective multiple
        /// </summary>
        [JsonPropertyName("effectiveMultiple")]
        public decimal? EffectiveMultiple { get; set; } = null;
        /// <summary>
        /// ["<c>initialRiskRatio</c>"] Initial risk ratio
        /// </summary>
        [JsonPropertyName("initialRiskRatio")]
        public decimal? InitialRiskRatio { get; set; } = null;
        /// <summary>
        /// ["<c>liquidationRiskRatio</c>"] Liquidation risk ratio
        /// </summary>
        [JsonPropertyName("liquidationRiskRatio")]
        public decimal? LiquidationRiskRatio { get; set; } = null;
        /// <summary>
        /// ["<c>baseAssetMaxBorrowable</c>"] Base asset max borrowable
        /// </summary>
        [JsonPropertyName("baseAssetMaxBorrowable")]
        public decimal? BaseAssetMaxBorrowable { get; set; } = null;
        /// <summary>
        /// ["<c>quoteAssetMaxBorrowable</c>"] Quote asset max borrowable
        /// </summary>
        [JsonPropertyName("quoteAssetMaxBorrowable")]
        public decimal? QuoteAssetMaxBorrowable { get; set; } = null;
    }
}

