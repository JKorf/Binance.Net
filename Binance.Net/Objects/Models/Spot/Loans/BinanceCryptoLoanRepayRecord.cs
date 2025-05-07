using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Repay record
    /// </summary>
    [SerializationModel]
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
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Repay timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
        /// <summary>
        /// Status of the repay
        /// </summary>
        [JsonPropertyName("repayStatus")]
        public BorrowStatus RepayStatus { get; set; }
        /// <summary>
        /// Collateral return
        /// </summary>
        [JsonPropertyName("collateralReturn")]
        public decimal CollateralReturn { get; set; }
        /// <summary>
        /// Collateral used
        /// </summary>
        [JsonPropertyName("collateralUsed")]
        public decimal CollateralUsed { get; set; }
        /// <summary>
        /// Repay quantity
        /// </summary>
        [JsonPropertyName("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// 1 for "repay with borrowed asset", 2 for "repay with collateral"
        /// </summary>
        [JsonPropertyName("repayType")]
        public int RepayType { get; set; }
    }
}
