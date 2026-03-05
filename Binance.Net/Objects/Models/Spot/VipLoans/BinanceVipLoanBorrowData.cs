namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan borrow data
    /// </summary>
    public record BinanceVipLoanBorrowData
    {
        /// <summary>
        /// The loan account identifier.
        /// </summary>
        [JsonPropertyName("loanAccountId")]
        public string LoanAccountId { get; set; } = string.Empty;
        /// <summary>
        /// The unique request identifier.
        /// </summary>
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;
        /// <summary>
        /// The asset that was borrowed.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Whether a flexible interest rate was selected.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("isFlexibleRate")]
        public bool IsFlexibleRate { get; set; }
        /// <summary>
        /// The borrowed quantity.
        /// </summary>
        [JsonPropertyName("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// Comma-separated collateral account identifiers.
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// Comma-separated collateral assets.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The loan term in days, when applicable.
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public decimal? LoanTerm { get; set; } = null;
    }
}
