namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan asset
    /// </summary>
    public record BinanceVipLoanAsset
    {
        /// <summary>
        /// ["<c>loanCoin</c>"] The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>_flexibleDailyInterestRate</c>"] Flexible daily interest rate
        /// </summary>
        [JsonPropertyName("_flexibleDailyInterestRate")]
        public decimal FlexibleDailyInterestRate { get; set; }
        /// <summary>
        /// ["<c>_flexibleYearlyInterestRate</c>"] Flexible yearly interest rate
        /// </summary>
        [JsonPropertyName("_flexibleYearlyInterestRate")]
        public decimal FlexibleYearlyInterestRate { get; set; }
        /// <summary>
        /// ["<c>_30dDailyInterestRate</c>"] Daily interest rate for 30 days
        /// </summary>
        [JsonPropertyName("_30dDailyInterestRate")]
        public decimal DailyInterestRate30Days { get; set; }
        /// <summary>
        /// ["<c>_30dYearlyInterestRate</c>"] Yearly interest rate for 30 days
        /// </summary>
        [JsonPropertyName("_30dYearlyInterestRate")]
        public decimal YearlyInterestRate30Days { get; set; }
        /// <summary>
        /// ["<c>_60dDailyInterestRate</c>"] Daily interest rate for 60 days
        /// </summary>
        [JsonPropertyName("_60dDailyInterestRate")]
        public decimal DailyInterestRate60Days { get; set; }
        /// <summary>
        /// ["<c>_60dYearlyInterestRate</c>"] Yearly interest rate for 60 days
        /// </summary>
        [JsonPropertyName("_60dYearlyInterestRate")]
        public decimal YearlyInterestRate60Days { get; set; }
        /// <summary>
        /// ["<c>minLimit</c>"] The minimum loan limit.
        /// </summary>
        [JsonPropertyName("minLimit")]
        public decimal MinLimit { get; set; }
        /// <summary>
        /// ["<c>maxLimit</c>"] The maximum loan limit.
        /// </summary>
        [JsonPropertyName("maxLimit")]
        public decimal MaxLimit { get; set; }
        /// <summary>
        /// ["<c>vipLevel</c>"] VIP level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
    }
}