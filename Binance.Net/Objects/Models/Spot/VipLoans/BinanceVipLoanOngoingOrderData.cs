namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan ongoing order data
    /// </summary>
    public record BinanceVipLoanOngoingOrderData
    {
        /// <summary>
        /// ["<c>orderId</c>"] The order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>loanCoin</c>"] The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalDebt</c>"] Loan debt quantity
        /// </summary>
        [JsonPropertyName("totalDebt")]
        public decimal LoanTotalDebt { get; set; }
        /// <summary>
        /// ["<c>residualInterest</c>"] Loan residual interest
        /// </summary>
        [JsonPropertyName("residualInterest")]
        public decimal ResidualInterest { get; set; }
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
        /// ["<c>totalCollateralValueAfterHaircut</c>"] Total collateral value after haircut.
        /// </summary>
        [JsonPropertyName("totalCollateralValueAfterHaircut")]
        public decimal TotalCollateralValueAfterHaircur { get; set; }
        /// <summary>
        /// ["<c>lockedCollateralValue</c>"] Locked collateral value
        /// </summary>
        [JsonPropertyName("lockedCollateralValue")]
        public decimal LockedCollateralValue { get; set; }
        /// <summary>
        /// ["<c>currentLTV</c>"] The current loan-to-value ratio.
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal LTV { get; set; }
        /// <summary>
        /// ["<c>expirationTime</c>"] Expiration time
        /// </summary>
        [JsonPropertyName("expirationTime")]
        public DateTime ExpirationTime { get; set; }
        /// <summary>
        /// ["<c>loanDate</c>"] The loan creation date and time.
        /// </summary>
        [JsonPropertyName("loanDate")]
        public DateTime LoanDate { get; set; }
        /// <summary>
        /// ["<c>loanTerm</c>"] The loan term.
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public string LoanTerm { get; set; } = string.Empty;
    }
}

