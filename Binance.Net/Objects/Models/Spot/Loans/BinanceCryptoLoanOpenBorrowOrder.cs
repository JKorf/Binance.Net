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
        [JsonProperty("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// The collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The collateral quantity
        /// </summary>
        [JsonProperty("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// Borrow order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Total debt
        /// </summary>
        public decimal TotalDebt { get; set; }
        /// <summary>
        /// Residual interest
        /// </summary>
        public decimal ResidualInterest { get; set; }
        /// <summary>
        /// Current LTV
        /// </summary>
        public decimal CurrentLTV { get; set; }
        /// <summary>
        /// Expiration time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExpirationTime { get; set; }
    }
}
