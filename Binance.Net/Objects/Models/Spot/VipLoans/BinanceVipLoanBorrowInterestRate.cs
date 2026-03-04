namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan interest rate
    /// </summary>
    public record BinanceVipLoanBorrowInterestRate
    {
        /// <summary>
        /// The loan asset.
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
        /// The timestamp of the interest rate snapshot.
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}
