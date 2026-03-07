namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan accured interest
    /// </summary>
    public record BinanceVipLoanAccuredInterest
    {
        /// <summary>
        /// ["<c>loanCoin</c>"] The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>principalAmount</c>"] Principal quantity
        /// </summary>
        [JsonPropertyName("principalAmount")]
        public decimal PrincipalQuantity { get; set; }
        /// <summary>
        /// ["<c>interestAmount</c>"] Interest quantity
        /// </summary>
        [JsonPropertyName("interestAmount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// ["<c>annualInterestRate</c>"] The annualized interest rate.
        /// </summary>
        [JsonPropertyName("annualInterestRate")]
        public decimal AnnualInterestRate { get; set; }
        /// <summary>
        /// ["<c>accrualTime</c>"] Accrual time
        /// </summary>
        [JsonPropertyName("accrualTime")]
        public DateTime AccrualTime { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] The latest renewal order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}

