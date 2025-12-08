namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan borrow data
    /// </summary>
    public record BinanceVipLoanBorrowData
    {
        /// <summary>
        /// Loan account id
        /// </summary>
        [JsonPropertyName("loanAccountId")]
        public string LoanAccountId { get; set; } = string.Empty;
        /// <summary>
        /// Request Id
        /// </summary>
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Is flexible rate
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("isFlexibleRate")]
        public bool IsFlexibleRate { get; set; }
        /// <summary>
        /// Loan quantity 
        /// </summary>
        [JsonPropertyName("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// Collateral accounts id separated by `,`
        /// </summary>
        [JsonPropertyName("collateralAccountId")]
        public string CollateralAccountId { get; set; } = string.Empty;
        /// <summary>
        /// Collateral assets separated by `,`
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Loan term
        /// </summary>
        [JsonPropertyName("loanTerm")]
        public decimal? LoanTerm { get; set; } = null;
    }
}
