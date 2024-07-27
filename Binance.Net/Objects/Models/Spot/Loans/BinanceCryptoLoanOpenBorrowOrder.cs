namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Open borrow order info
    /// </summary>
    public record BinanceCryptoLoanOpenBorrowOrder
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
        /// The collateral quantity
        /// </summary>
        [JsonPropertyName("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// Borrow order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Total debt
        /// </summary>
        [JsonPropertyName("totalDebt")]
        public decimal TotalDebt { get; set; }
        /// <summary>
        /// Residual interest
        /// </summary>
        [JsonPropertyName("residualInterest")]
        public decimal ResidualInterest { get; set; }
        /// <summary>
        /// Current LTV
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal CurrentLTV { get; set; }
        /// <summary>
        /// Expiration time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("expirationTime")]
        public DateTime ExpirationTime { get; set; }
    }
}
