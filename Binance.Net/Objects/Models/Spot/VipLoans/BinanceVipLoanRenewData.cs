namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan renew data
    /// </summary>
    public record BinanceVipLoanRenewData
    {
        /// <summary>
        /// ["<c>loanAccountId</c>"] The loan account identifier.
        /// </summary>
        [JsonPropertyName("loanAccountId")]
        public string LoanAccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>loanCoin</c>"] The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>loanAmount</c>"] Loan quantity
        /// </summary>
        [JsonPropertyName("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// ["<c>collateralAccountId</c>"] Comma-separated collateral account identifiers.
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralCoin</c>"] Comma-separated collateral assets.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>loanTerm</c>"] The renewed loan term in days.
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public decimal LoanTerm { get; set; }
    }
}

