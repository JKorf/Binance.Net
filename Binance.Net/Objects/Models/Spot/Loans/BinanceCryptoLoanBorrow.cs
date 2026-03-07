using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Borrow info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanBorrow
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
        /// ["<c>loanAmount</c>"] The loan quantity
        /// </summary>
        [JsonPropertyName("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// ["<c>collateralAmount</c>"] The collateral quantity
        /// </summary>
        [JsonPropertyName("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] The borrow status.
        /// </summary>
        [JsonPropertyName("status")]
        public FlexibleBorrowStatus Status { get; set; }
    }
}

