namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan interest rate
    /// </summary>
    public record BinanceVipLoanBorrowInterestRate
    {
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Flexible daily interest rate
        /// </summary>
        [JsonPropertyName("flexibleDailyInterestRate")]
        public decimal FlexibleDailyInterestRate { get; set; }
        /// <summary>
        /// Flexible yearly interest rate
        /// </summary>
        [JsonPropertyName("flexibleYearlyInterestRate")]
        public decimal FlexibleYearlyInterestRate { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}
