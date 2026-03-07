namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan repayment data
    /// </summary>
    public record BinanceVipLoanCollateralAccountLockedValue
    {
        /// <summary>
        /// ["<c>collateralAccountId</c>"] The collateral account identifier.
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralCoin</c>"] Comma-separated collateral assets.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
    }
}

