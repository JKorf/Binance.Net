namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan ongoing order data
    /// </summary>
    public record BinanceVipLoanOngoingOrderData
    {
        /// <summary>
        /// The order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Loan debt quantity
        /// </summary>
        [JsonPropertyName("totalDebt")]
        public decimal LoanTotalDebt { get; set; }
        /// <summary>
        /// Loan residual interest
        /// </summary>
        [JsonPropertyName("residualInterest")]
        public decimal ResidualInterest { get; set; }
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
        /// Total collateral value after haircut.
        /// </summary>
        [JsonPropertyName("totalCollateralValueAfterHaircut")]
        public decimal TotalCollateralValueAfterHaircur { get; set; }
        /// <summary>
        /// Locked collateral value
        /// </summary>
        [JsonPropertyName("lockedCollateralValue")]
        public decimal LockedCollateralValue { get; set; }
        /// <summary>
        /// The current loan-to-value ratio.
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal LTV { get; set; }
        /// <summary>
        /// Expiration time
        /// </summary>
        [JsonPropertyName("expirationTime")]
        public DateTime ExpirationTime { get; set; }
        /// <summary>
        /// The loan creation date and time.
        /// </summary>
        [JsonPropertyName("loanDate")]
        public DateTime LoanDate { get; set; }
        /// <summary>
        /// The loan term.
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public string LoanTerm { get; set; } = string.Empty;
    }
}
