namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan borrow data
    /// </summary>
    public record BinanceVipLoanBorrowData
    {
        /// <summary>
        /// ["<c>loanAccountId</c>"] The loan account identifier.
        /// </summary>
        [JsonPropertyName("loanAccountId")]
        public string LoanAccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>requestId</c>"] The unique request identifier.
        /// </summary>
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>loanCoin</c>"] The asset that was borrowed.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isFlexibleRate</c>"] Whether a flexible interest rate was selected.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("isFlexibleRate")]
        public bool IsFlexibleRate { get; set; }
        /// <summary>
        /// ["<c>loanAmount</c>"] The borrowed quantity.
        /// </summary>
        [JsonPropertyName("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// ["<c>collateralAccountId</c>"] Comma-separated collateral account identifiers.
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralCoin</c>"] Comma-separated collateral assets.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>loanTerm</c>"] The loan term in days, when applicable.
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public decimal? LoanTerm { get; set; } = null;
    }
}

