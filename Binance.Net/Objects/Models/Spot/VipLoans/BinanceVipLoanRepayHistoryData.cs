using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan repayment data
    /// </summary>
    public record BinanceVipLoanRepayHistoryData
    {
        /// <summary>
        /// ["<c>loanCoin</c>"] The repaid loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>repayAmount</c>"] The repaid quantity.
        /// </summary>
        [JsonPropertyName("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// ["<c>collateralCoin</c>"] Comma-separated collateral assets.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>repayStatus</c>"] The repayment status.
        /// </summary>
        [JsonPropertyName("repayStatus")]
        public VipLoanRepayStatus RepayStatus { get; set; }
        /// <summary>
        /// ["<c>loanDate</c>"] The loan creation date and time.
        /// </summary>
        [JsonPropertyName("loanDate")]
        public DateTime LoanDate { get; set; }
        /// <summary>
        /// ["<c>repayTime</c>"] The repayment date and time.
        /// </summary>
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] The order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}

