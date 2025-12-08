namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan accured interest
    /// </summary>
    public record BinanceVipLoanAccuredInterest
    {
        /// <summary>
        /// Loan asset
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
        /// Annual interest rate
        /// </summary>
        [JsonPropertyName("annualInterestRate")]
        public decimal AnnualInterestRate { get; set; }
        /// <summary>
        /// Accrual time
        /// </summary>
        [JsonPropertyName("accrualTime")]
        public DateTime AccrualTime { get; set; }
        /// <summary>
        /// Latest order ID for renewal order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}
