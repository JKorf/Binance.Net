using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Loanable asset info
    /// </summary>
    public class BinanceCryptoLoanAsset
    {
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonProperty("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Hourly interest rate for 7 days
        /// </summary>
        [JsonProperty("_7dHourlyInterestRate")]
        public decimal HourlyInterest7Days { get; set; }

        /// <summary>
        /// Daily interest rate for 7 days
        /// </summary>
        [JsonProperty("_7dDailyInterestRate")]
        public decimal DailyInterest7Days { get; set; }
        /// <summary>
        /// Hourly interest rate for 14 days
        /// </summary>
        [JsonProperty("_14dHourlyInterestRate")]
        public decimal HourlyInterest14Days { get; set; }

        /// <summary>
        /// Daily interest rate for 14 days
        /// </summary>
        [JsonProperty("_14dDailyInterestRate")]
        public decimal DailyInterest14Days { get; set; }
        /// <summary>
        /// Daily interest rate for 30 days
        /// </summary>
        [JsonProperty("_30dHourlyInterestRate")]
        public decimal HourlyInterest30Days { get; set; }
        /// <summary>
        /// Daily interest rate for 30 days
        /// </summary>
        [JsonProperty("_30dDailyInterestRate")]
        public decimal DailyInterest30Days { get; set; }
        /// <summary>
        /// Daily interest rate for 90 days
        /// </summary>
        [JsonProperty("_90dHourlyInterestRate")]
        public decimal HourlyInterest90Days { get; set; }
        /// <summary>
        /// Daily interest rate for 90 days
        /// </summary>
        [JsonProperty("_90dDailyInterestRate")]
        public decimal DailyInterest90Days { get; set; }
        /// <summary>
        /// Daily interest rate for 180 days
        /// </summary>
        [JsonProperty("_180dHourlyInterestRate")]
        public decimal HourlyInterest180Days { get; set; }
        /// <summary>
        /// Daily interest rate for 180 days
        /// </summary>
        [JsonProperty("_180dDailyInterestRate")]
        public decimal DailyInterest180Days { get; set; }

        /// <summary>
        /// Min limit
        /// </summary>
        [JsonProperty("minLimit")]
        public decimal MinLimit { get; set; }
        /// <summary>
        /// Min limit
        /// </summary>
        [JsonProperty("maxLimit")]
        public decimal MaxLimit { get; set; }
        /// <summary>
        /// Vip level
        /// </summary>
        [JsonProperty("vipLevel")]
        public int VipLevel { get; set; }
    }
}
