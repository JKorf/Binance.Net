namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Cross margin collateral info
    /// </summary>
    public class BinanceCrossMarginCollateralRatio
    {
        /// <summary>
        /// Collaterals
        /// </summary>
        public IEnumerable<BinanceCrossMarginCollateral> Collaterals { get; set; } = Array.Empty<BinanceCrossMarginCollateral>();
        /// <summary>
        /// Asset names
        /// </summary>
        public IEnumerable<string> AssetNames { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// Collateral info
    /// </summary>
    public class BinanceCrossMarginCollateral
    {
        /// <summary>
        /// Min usd value
        /// </summary>
        public decimal MinUsdValue { get; set; }
        /// <summary>
        /// Max usd value
        /// </summary>
        public decimal? MaxUsdValue { get; set; }
        /// <summary>
        /// Discount rate
        /// </summary>
        public decimal DiscountRate { get; set; }
    }
}
