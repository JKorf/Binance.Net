namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Isolated margin tier data
    /// </summary>
    public record BinanceIsolatedMarginTierData
    {
        /// <summary>
        /// Average price
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
        public decimal EffectiveMultiple { get; set; }
        /// <summary>
        /// Initial risk ratio
        /// </summary>
        [JsonPropertyName("initialRiskRatio")]
        public decimal InitialRiskRatio { get; set; }
        /// <summary>
        /// Liquidation risk ratio
        /// </summary>
        [JsonPropertyName("liquidationRiskRatio")]
        public decimal LiquidationRiskRatio { get; set; }
        /// <summary>
        /// Base asset max borrowable
        /// </summary>
        [JsonPropertyName("baseAssetMaxBorrowable")]
        public decimal BaseAssetMaxBorrowable { get; set; }
        /// <summary>
        /// Quote asset max borrowable
        /// </summary>
        [JsonPropertyName("quoteAssetMaxBorrowable")]
        public decimal QuoteAssetMaxBorrowable { get; set; }
    }
}
