namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Repay rate info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanRepayRate
    {
        /// <summary>
        /// The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// The collateral asset.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
    }
}
