namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan repayment data
    /// </summary>
    public record BinanceVipLoanCollateralAccountLockedValue
    {
        /// <summary>
        /// Collateral account id
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// Collateral assets separated by `,`
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
    }
}
