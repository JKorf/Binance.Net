using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Borrow record
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanBorrowRecord
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
        /// ["<c>hourlyInterestRate</c>"] Hourly interest rate
        /// </summary>
        [JsonPropertyName("hourlyInterestRate")]
        public decimal HourlyInterestRate { get; set; }
        /// <summary>
        /// ["<c>loanTerm</c>"] Loan term
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public int LoanTerm { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] The borrow order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>borrowTime</c>"] Borrow timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("borrowTime")]
        public DateTime BorrowTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public BorrowStatus Status { get; set; }
    }
}

