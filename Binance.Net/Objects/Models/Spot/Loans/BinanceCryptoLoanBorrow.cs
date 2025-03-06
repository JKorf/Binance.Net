namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Borrow info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanBorrow
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
        [JsonPropertyName("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// The collateral quantity
        /// </summary>
        [JsonPropertyName("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// Hourly interest rate
        /// </summary>
        [JsonPropertyName("hourlyInterestRate")]
        public decimal HourlyInterestRate { get; set; }
        /// <summary>
        /// Borrow order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
    }
}
