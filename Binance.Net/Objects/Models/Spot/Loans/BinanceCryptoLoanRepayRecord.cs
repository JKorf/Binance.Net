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
        /// ["<c>orderId</c>"] Borrow order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>repayTime</c>"] Repay timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
        /// <summary>
        /// ["<c>repayStatus</c>"] The repayment status.
        /// </summary>
        [JsonPropertyName("repayStatus")]
        public BorrowStatus RepayStatus { get; set; }
        /// <summary>
        /// ["<c>collateralReturn</c>"] Collateral return
        /// </summary>
        [JsonPropertyName("collateralReturn")]
        public decimal CollateralReturn { get; set; }
        /// <summary>
        /// ["<c>collateralUsed</c>"] Collateral used
        /// </summary>
        [JsonPropertyName("collateralUsed")]
        public decimal CollateralUsed { get; set; }
        /// <summary>
        /// ["<c>repayAmount</c>"] Repay quantity
        /// </summary>
        [JsonPropertyName("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// ["<c>repayType</c>"] 1 for "repay with borrowed asset", 2 for "repay with collateral"
        /// </summary>
        [JsonPropertyName("repayType")]
        public int RepayType { get; set; }
    }
}

