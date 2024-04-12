using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Borrow record
    /// </summary>
    public class BinanceCryptoLoanBorrowRecord
    {
        /// <summary>
        /// The loaning asset
        /// </summary>
        [JsonProperty("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// The collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The loan quantity
        /// </summary>
        [JsonProperty("initialLoanAmount")]
        public decimal InitialLoanQuantity { get; set; }
        /// <summary>
        /// The collateral quantity
        /// </summary>
        [JsonProperty("initialCollateralAmount")]
        public decimal InitialCollateralQuantity { get; set; }
        /// <summary>
        /// Hourly interest rate
        /// </summary>
        public decimal HourlyInterestRate { get; set; }
        /// <summary>
        /// Loan term
        /// </summary>
        public int LoanTerm { get; set; }
        /// <summary>
        /// Borrow order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Borrow timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime BorrowTime { get; set; }
        /// <summary>
        /// Status of the order
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public BorrowStatus Status { get; set; }
    }
}
