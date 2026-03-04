namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan renew data
    /// </summary>
    public record BinanceVipLoanRenewData
    {
        /// <summary>
        /// The loan account identifier.
        /// </summary>
        [JsonPropertyName("loanAccountId")]
        public string LoanAccountId { get; set; } = string.Empty;
        /// <summary>
        /// The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Loan quantity
        /// </summary>
        [JsonPropertyName("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// Comma-separated collateral account identifiers.
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// Comma-separated collateral assets.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The renewed loan term in days.
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public decimal LoanTerm { get; set; }
    }
}
