namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan interest rate
    /// </summary>
    public record BinanceVipLoanBorrowInterestRate
    {
        /// <summary>
        /// ["<c>asset</c>"] The loan asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>flexibleDailyInterestRate</c>"] Flexible daily interest rate
        /// </summary>
        [JsonPropertyName("flexibleDailyInterestRate")]
        public decimal FlexibleDailyInterestRate { get; set; }
        /// <summary>
        /// ["<c>flexibleYearlyInterestRate</c>"] Flexible yearly interest rate
        /// </summary>
        [JsonPropertyName("flexibleYearlyInterestRate")]
        public decimal FlexibleYearlyInterestRate { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The timestamp of the interest rate snapshot.
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}

