namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan accured interest
    /// </summary>
    public record BinanceVipLoanAccuredInterest
    {
        /// <summary>
        /// The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Principal quantity
        /// </summary>
        [JsonPropertyName("principalAmount")]
        public decimal PrincipalQuantity { get; set; }
        /// <summary>
        /// Interest quantity
        /// </summary>
        [JsonPropertyName("interestAmount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// The annualized interest rate.
        /// </summary>
        [JsonPropertyName("annualInterestRate")]
        public decimal AnnualInterestRate { get; set; }
        /// <summary>
        /// Accrual time
        /// </summary>
        [JsonPropertyName("accrualTime")]
        public DateTime AccrualTime { get; set; }
        /// <summary>
        /// The latest renewal order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}
