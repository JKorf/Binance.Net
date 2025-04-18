namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Loanable asset info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanAsset
    {
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Hourly interest rate for 7 days
        /// </summary>
        [JsonPropertyName("_7dHourlyInterestRate")]
        public decimal HourlyInterest7Days { get; set; }

        /// <summary>
        /// Daily interest rate for 7 days
        /// </summary>
        [JsonPropertyName("_7dDailyInterestRate")]
        public decimal DailyInterest7Days { get; set; }
        /// <summary>
        /// Hourly interest rate for 14 days
        /// </summary>
        [JsonPropertyName("_14dHourlyInterestRate")]
        public decimal HourlyInterest14Days { get; set; }

        /// <summary>
        /// Daily interest rate for 14 days
        /// </summary>
        [JsonPropertyName("_14dDailyInterestRate")]
        public decimal DailyInterest14Days { get; set; }
        /// <summary>
        /// Daily interest rate for 30 days
        /// </summary>
        [JsonPropertyName("_30dHourlyInterestRate")]
        public decimal HourlyInterest30Days { get; set; }
        /// <summary>
        /// Daily interest rate for 30 days
        /// </summary>
        [JsonPropertyName("_30dDailyInterestRate")]
        public decimal DailyInterest30Days { get; set; }
        /// <summary>
        /// Daily interest rate for 90 days
        /// </summary>
        [JsonPropertyName("_90dHourlyInterestRate")]
        public decimal HourlyInterest90Days { get; set; }
        /// <summary>
        /// Daily interest rate for 90 days
        /// </summary>
        [JsonPropertyName("_90dDailyInterestRate")]
        public decimal DailyInterest90Days { get; set; }
        /// <summary>
        /// Daily interest rate for 180 days
        /// </summary>
        [JsonPropertyName("_180dHourlyInterestRate")]
        public decimal HourlyInterest180Days { get; set; }
        /// <summary>
        /// Daily interest rate for 180 days
        /// </summary>
        [JsonPropertyName("_180dDailyInterestRate")]
        public decimal DailyInterest180Days { get; set; }

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
        /// Vip level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
    }
}
