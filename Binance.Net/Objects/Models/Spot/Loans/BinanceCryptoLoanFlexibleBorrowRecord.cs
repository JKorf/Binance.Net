using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Borrow record
    /// </summary>
    public record BinanceCryptoLoanFlexibleBorrowRecord
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
        /// ["<c>initialLoanAmount</c>"] The loan quantity
        /// </summary>
        [JsonPropertyName("initialLoanAmount")]
        public decimal InitialLoanQuantity { get; set; }
        /// <summary>
        /// ["<c>initialCollateralAmount</c>"] The collateral quantity
        /// </summary>
        [JsonPropertyName("initialCollateralAmount")]
        public decimal InitialCollateralQuantity { get; set; }
        /// <summary>
        /// ["<c>borrowTime</c>"] Borrow timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("borrowTime")]
        public DateTime BorrowTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] The borrow record status.
        /// </summary>
        [JsonPropertyName("status")]
        public FlexibleBorrowRecordStatus Status { get; set; }
    }
}

