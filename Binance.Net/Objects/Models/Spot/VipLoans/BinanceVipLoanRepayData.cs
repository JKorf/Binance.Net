using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan repay data
    /// </summary>
    public record BinanceVipLoanRepayData
    {
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Repay quantity
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
        /// Collateral assets separated by `,`
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Current LTV
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal LTV { get; set; }
        /// <summary>
        /// Repay status
        /// </summary>
        [JsonPropertyName("repayStatus")]
        public VipLoanRepayStatus RepayStatus { get; set; }
    }
}
