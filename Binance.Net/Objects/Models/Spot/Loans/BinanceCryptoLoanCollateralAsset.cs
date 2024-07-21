namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Collateral asset info
    /// </summary>
    public record BinanceCryptoLoanCollateralAsset
    {
        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string ColleteralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Initial ltv
        /// </summary>
        [JsonPropertyName("initialLTV")]
        public decimal InitialLtv { get; set; }
        /// <summary>
        /// Margin call ltv
        /// </summary>
        [JsonPropertyName("marginCallLTV")]
        public decimal MarginCallLtv { get; set; }
        /// <summary>
        /// Liquidation ltv
        /// </summary>
        [JsonPropertyName("liquidationLTV")]
        public decimal LiquidationLtv { get; set; }
        /// <summary>
        /// Max limit
        /// </summary>
        [JsonPropertyName("maxLimit")]
        public decimal MaxLimit { get; set; }
        /// <summary>
        /// Vip level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
    }
}
