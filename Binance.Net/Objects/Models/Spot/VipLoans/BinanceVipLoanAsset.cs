namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan asset
    /// </summary>
    public record BinanceVipLoanAsset
    {
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Flexible daily interest rate
        /// </summary>
        [JsonPropertyName("_flexibleDailyInterestRate")]
        public decimal FlexibleDailyInterestRate { get; set; }
        /// <summary>
        /// Flexible yearly interest rate
        /// </summary>
        [JsonPropertyName("_flexibleYearlyInterestRate")]
        public decimal FlexibleYearlyInterestRate { get; set; }
        /// <summary>
        /// Daily interest rate for 30 days
        /// </summary>
        [JsonPropertyName("_30dDailyInterestRate")]
        public decimal DailyInterestRate30Days { get; set; }
        /// <summary>
        /// Yearly interest rate for 30 days
        /// </summary>
        [JsonPropertyName("_30dYearlyInterestRate")]
        public decimal YearlyInterestRate30Days { get; set; }
        /// <summary>
        /// Daily interest rate for 60 days
        /// </summary>
        [JsonPropertyName("_60dDailyInterestRate")]
        public decimal DailyInterestRate60Days { get; set; }
        /// <summary>
        /// Yearly interest rate for 60 days
        /// </summary>
        [JsonPropertyName("_60dYearlyInterestRate")]
        public decimal YearlyInterestRate60Days { get; set; }
        /// <summary>
        /// Min limit
        /// </summary>
        [JsonPropertyName("minLimit")]
        public decimal MinLimit { get; set; }
        /// <summary>
        /// Min limit
        /// </summary>
        [JsonPropertyName("maxLimit")]
        public decimal MaxLimit { get; set; }
        /// <summary>
        /// VIP level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
    }
}