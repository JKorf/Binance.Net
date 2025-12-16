using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Repay info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanRepay
    {
        /// <summary>
        /// The loaning asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// The collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Remaining debt
        /// </summary>
        [JsonPropertyName("remainingDebt")]
        public decimal? RemainingDebt { get; set; }
        /// <summary>
        /// Remaining collateral
        /// </summary>
        [JsonPropertyName("remainingCollateral")]
        public decimal? RemainingCollateral { get; set; }
        /// <summary>
        /// Fully repaid
        /// </summary>
        [JsonPropertyName("fullRepayment")]
        public bool FullRepayment{ get; set; }
        /// <summary>
        /// Current LTV
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal? CurrentLTV { get; set; }
        /// <summary>
        /// Repay status
        /// </summary>
        [JsonPropertyName("repayStatus")]
        public RepayStatus RepayStatus { get; set; }
    }
}
