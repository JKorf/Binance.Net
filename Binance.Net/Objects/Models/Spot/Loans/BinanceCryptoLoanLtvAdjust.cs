namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Adjust info
    /// </summary>
    public record BinanceCryptoLoanLtvAdjust
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
        /// Direction
        /// </summary>
        public string Direction { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Current ltv
        /// </summary>
        public decimal CurrentLtv { get; set; }
    }
}
