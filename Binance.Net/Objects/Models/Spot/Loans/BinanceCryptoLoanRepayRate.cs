namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Repay rate info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanRepayRate
    {
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Repay quantity
        /// </summary>
        [JsonPropertyName("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
    }
}
