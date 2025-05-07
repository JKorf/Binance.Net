using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan application status
    /// </summary>
    public record BinanceVipLoanApplicationStatus
    {
        /// <summary>
        /// ID of account receiving loan
        /// </summary>
        [JsonPropertyName("loanAccountId")]
        public string LoanAccountId { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Request id
        /// </summary>
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Loan quantity 
        /// </summary>
        [JsonPropertyName("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// Collateral account id separated by `,`
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// Collateral assets separated by `,`
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Loan term
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public string LoanTerm { get; set; } = string.Empty;
        /// <summary>
        /// Application status
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(EnumConverter))]
        public VipLoanApplicationStatus Status { get; set; }
        /// <summary>
        /// Loan date
        /// </summary>
        [JsonPropertyName("loanDate")]
        public DateTime LoanDate { get; set; }
    }
}
