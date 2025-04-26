using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Flexible repay record
    /// </summary>
    public record BinanceCryptoLoanFlexibleRepayRecord
    {
        /// <summary>
        /// The loaning asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Repay quantity
        /// </summary>
        [JsonPropertyName("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// The collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Collateral return
        /// </summary>
        [JsonPropertyName("collateralReturn")]
        public decimal CollateralReturn { get; set; }
        /// <summary>
        /// Status of the repay
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("repayStatus")]
        public RepayStatus RepayStatus { get; set; }
        /// <summary>
        /// Repay timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
    }
}
