namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Collateral asset info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanCollateralAsset
    {
        /// <summary>
        /// ["<c>collateralCoin</c>"] The collateral asset.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>initialLTV</c>"] Initial ltv
        /// </summary>
        [JsonPropertyName("initialLTV")]
        public decimal InitialLtv { get; set; }
        /// <summary>
        /// ["<c>marginCallLTV</c>"] Margin call ltv
        /// </summary>
        [JsonPropertyName("marginCallLTV")]
        public decimal MarginCallLtv { get; set; }
        /// <summary>
        /// ["<c>liquidationLTV</c>"] Liquidation ltv
        /// </summary>
        [JsonPropertyName("liquidationLTV")]
        public decimal LiquidationLtv { get; set; }
        /// <summary>
        /// ["<c>maxLimit</c>"] The maximum collateral limit.
        /// </summary>
        [JsonPropertyName("maxLimit")]
        public decimal MaxLimit { get; set; }
    }
}

