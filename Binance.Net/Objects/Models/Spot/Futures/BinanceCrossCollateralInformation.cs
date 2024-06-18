namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Collateral info
    /// </summary>
    public record BinanceCrossCollateralInformation
    {
        /// <summary>
        /// The loan asset
        /// </summary>
        [JsonProperty("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// The collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// Margin call collateral rate
        /// </summary>
        public decimal MarginCallCollateralRate { get; set; }
        /// <summary>
        /// Liquidation collateal rate
        /// </summary>
        public decimal LiquidationCollateralRate { get; set; }
        /// <summary>
        /// Current collateral rate
        /// </summary>
        public decimal CurrentCollateralRate { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// In days
        /// </summary>
        public int InterestGracePeriod { get; set; }
    }
}
