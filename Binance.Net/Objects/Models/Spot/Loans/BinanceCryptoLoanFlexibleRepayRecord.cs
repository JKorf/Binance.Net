using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Flexible repay record
    /// </summary>
    public record BinanceCryptoLoanFlexibleRepayRecord
    {
        /// <summary>
        /// ["<c>loanCoin</c>"] The loaning asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>repayAmount</c>"] Repay quantity
        /// </summary>
        [JsonPropertyName("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// ["<c>collateralCoin</c>"] The collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralReturn</c>"] Collateral return
        /// </summary>
        [JsonPropertyName("collateralReturn")]
        public decimal CollateralReturn { get; set; }
        /// <summary>
        /// ["<c>repayStatus</c>"] Status of the repay
        /// </summary>
        [JsonPropertyName("repayStatus")]
        public RepayStatus RepayStatus { get; set; }
        /// <summary>
        /// ["<c>repayTime</c>"] The repayment timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
    }
}

