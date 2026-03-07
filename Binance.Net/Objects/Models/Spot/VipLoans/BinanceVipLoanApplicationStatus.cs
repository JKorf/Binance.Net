using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan application status
    /// </summary>
    public record BinanceVipLoanApplicationStatus
    {
        /// <summary>
        /// ["<c>loanAccountId</c>"] The identifier of the account receiving the loan.
        /// </summary>
        [JsonPropertyName("loanAccountId")]
        public string LoanAccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] The order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>requestId</c>"] The request identifier.
        /// </summary>
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>loanCoin</c>"] The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>loanAmount</c>"] The loan quantity.
        /// </summary>
        [JsonPropertyName("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// ["<c>collateralAccountId</c>"] Comma-separated collateral account identifiers.
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralCoin</c>"] Comma-separated collateral assets.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>loanTerm</c>"] The loan term.
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public string LoanTerm { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] The current application status.
        /// </summary>
        [JsonPropertyName("status")]
        public VipLoanApplicationStatus Status { get; set; }
        /// <summary>
        /// ["<c>loanDate</c>"] The loan creation date and time.
        /// </summary>
        [JsonPropertyName("loanDate")]
        public DateTime LoanDate { get; set; }
    }
}

