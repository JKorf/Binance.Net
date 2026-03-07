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
        /// ["<c>loanCoin</c>"] The loaning asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralCoin</c>"] The collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>remainingDebt</c>"] Remaining debt
        /// </summary>
        [JsonPropertyName("remainingDebt")]
        public decimal? RemainingDebt { get; set; }
        /// <summary>
        /// ["<c>remainingCollateral</c>"] Remaining collateral
        /// </summary>
        [JsonPropertyName("remainingCollateral")]
        public decimal? RemainingCollateral { get; set; }
        /// <summary>
        /// ["<c>fullRepayment</c>"] Whether the loan is fully repaid.
        /// </summary>
        [JsonPropertyName("fullRepayment")]
        public bool FullRepayment{ get; set; }
        /// <summary>
        /// ["<c>currentLTV</c>"] Current LTV
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal? CurrentLTV { get; set; }
        /// <summary>
        /// ["<c>repayStatus</c>"] Repay status
        /// </summary>
        [JsonPropertyName("repayStatus")]
        public RepayStatus RepayStatus { get; set; }
    }
}

