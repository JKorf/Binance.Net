namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan ongoing order data
    /// </summary>
    public record BinanceVipLoanOngoingOrderData
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Loan asset
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
        /// Collateral account id separated by `,`
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// Collateral assets separated by `,`
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Total collateral value after haircur
        /// </summary>
        [JsonPropertyName("totalCollateralValueAfterHaircut")]
        public decimal TotalCollateralValueAfterHaircur { get; set; }
        /// <summary>
        /// Locked collateral value
        /// </summary>
        [JsonPropertyName("lockedCollateralValue")]
        public decimal LockedCollateralValue { get; set; }
        /// <summary>
        /// Current LTV
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal LTV { get; set; }
        /// <summary>
        /// Expiration time
        /// </summary>
        [JsonPropertyName("expirationTime")]
        public DateTime ExpirationTime { get; set; }
        /// <summary>
        /// Loan date
        /// </summary>
        [JsonPropertyName("loanDate")]
        public DateTime LoanDate { get; set; }
        /// <summary>
        /// Loan term
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public string LoanTerm { get; set; } = string.Empty;
    }
}
