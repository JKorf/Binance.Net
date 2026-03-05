using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan repay data
    /// </summary>
    public record BinanceVipLoanRepayData
    {
        /// <summary>
        /// The repaid loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// The repaid quantity.
        /// </summary>
        [JsonPropertyName("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// Remaining principal
        /// </summary>
        [JsonPropertyName("remainingPrincipal")]
        public decimal RemainingPrincipal { get; set; }
        /// <summary>
        /// Remaining interest
        /// </summary>
        [JsonPropertyName("remainingInterest")]
        public decimal RemainingInterest { get; set; }
        /// <summary>
        /// Comma-separated collateral assets.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The current loan-to-value ratio.
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal LTV { get; set; }
        /// <summary>
        /// The repayment status.
        /// </summary>
        [JsonPropertyName("repayStatus")]
        public VipLoanRepayStatus RepayStatus { get; set; }
    }
}
