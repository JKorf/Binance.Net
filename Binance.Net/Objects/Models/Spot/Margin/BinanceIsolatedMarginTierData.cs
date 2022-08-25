namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Isolated margin tier data
    /// </summary>
    public class BinanceIsolatedMarginTierData
    {
        /// <summary>
        /// Average price
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Tier
        /// </summary>
        public int Tier { get; set; }
        /// <summary>
        /// Effective multiple
        /// </summary>
        public decimal EffectiveMultiple { get; set; }
        /// <summary>
        /// Initial risk ratio
        /// </summary>
        public decimal InitialRiskRatio { get; set; }
        /// <summary>
        /// Liquidation risk ratio
        /// </summary>
        public decimal LiquidationRiskRatio { get; set; }
        /// <summary>
        /// Base asset max borrowable
        /// </summary>
        public decimal BaseAssetMaxBorrowable { get; set; }
        /// <summary>
        /// Quote asset max borrowable
        /// </summary>
        public decimal QuoteAssetMaxBorrowable { get; set; }
    }
}
