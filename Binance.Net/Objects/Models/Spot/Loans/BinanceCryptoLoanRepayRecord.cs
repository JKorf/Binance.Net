using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Repay record
    /// </summary>
    public record BinanceCryptoLoanRepayRecord
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
        /// Borrow order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Repay timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime RepayTime { get; set; }
        /// <summary>
        /// Status of the repay
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public BorrowStatus RepayStatus { get; set; }
        /// <summary>
        /// Collateral return
        /// </summary>
        public decimal CollateralReturn { get; set; }
        /// <summary>
        /// Collateral used
        /// </summary>
        public decimal CollateralUsed { get; set; }
        /// <summary>
        /// Repay quantity
        /// </summary>
        [JsonPropertyName("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// 1 for "repay with borrowed asset", 2 for "repay with collateral"
        /// </summary>
        public int RepayType { get; set; }
    }
}
