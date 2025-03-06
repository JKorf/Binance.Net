using Binance.Net.Converters;
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
        /// Hourly interest rate
        /// </summary>
        [JsonPropertyName("hourlyInterestRate")]
        public decimal HourlyInterestRate { get; set; }
        /// <summary>
        /// Loan term
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public int LoanTerm { get; set; }
        /// <summary>
        /// Borrow order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Borrow timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("borrowTime")]
        public DateTime BorrowTime { get; set; }
        /// <summary>
        /// Status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public BorrowStatus Status { get; set; }
    }
}
