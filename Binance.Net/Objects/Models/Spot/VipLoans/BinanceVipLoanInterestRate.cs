namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan interest rate
    /// </summary>
    public record BinanceVipLoanInterestRate
    {
        /// <summary>
        /// ["<c>coin</c>"] The loan asset.
        /// </summary>
        [JsonPropertyName("coin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>annualizedInterestRate</c>"] Annualized interest rate
        /// </summary>
        [JsonPropertyName("annualizedInterestRate")]
        public decimal AnnualizedInterestRate { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The timestamp of the interest rate snapshot.
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}

