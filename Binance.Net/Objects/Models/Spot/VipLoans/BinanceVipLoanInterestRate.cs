namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan interest rate
    /// </summary>
    public record BinanceVipLoanInterestRate
    {
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Annualized interest rate
        /// </summary>
        [JsonPropertyName("annualizedInterestRate")]
        public decimal AnnualizedInterestRate { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}
