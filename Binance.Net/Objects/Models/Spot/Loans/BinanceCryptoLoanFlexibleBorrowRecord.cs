using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Borrow record
    /// </summary>
    public record BinanceCryptoLoanFlexibleBorrowRecord
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
        /// The loan quantity
        /// </summary>
        [JsonPropertyName("initialLoanAmount")]
        public decimal InitialLoanQuantity { get; set; }
        /// <summary>
        /// The collateral quantity
        /// </summary>
        [JsonPropertyName("initialCollateralAmount")]
        public decimal InitialCollateralQuantity { get; set; }
        /// <summary>
        /// Borrow timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("borrowTime")]
        public DateTime BorrowTime { get; set; }
        /// <summary>
        /// Status of the order
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("status")]
        public FlexibleBorrowRecordStatus Status { get; set; }
    }
}
