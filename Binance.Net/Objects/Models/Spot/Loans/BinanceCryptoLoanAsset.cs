namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Loanable asset info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanAsset
    {
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Interest rate
        /// </summary>
        [JsonPropertyName("flexibleInterestRate")]
        public decimal InterestRate{ get; set; }
        /// <summary>
        /// Min limit
        /// </summary>
        [JsonPropertyName("flexibleMinLimit")]
        public decimal MinLimit { get; set; }
        /// <summary>
        /// Max limit
        /// </summary>
        [JsonPropertyName("flexibleMaxLimit")]
        public decimal MaxLimit { get; set; }
    }
}
