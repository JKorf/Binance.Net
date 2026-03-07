namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Loanable asset info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanAsset
    {
        /// <summary>
        /// ["<c>loanCoin</c>"] The loan asset.
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>flexibleInterestRate</c>"] The flexible interest rate.
        /// </summary>
        [JsonPropertyName("flexibleInterestRate")]
        public decimal InterestRate{ get; set; }
        /// <summary>
        /// ["<c>flexibleMinLimit</c>"] Min limit
        /// </summary>
        [JsonPropertyName("flexibleMinLimit")]
        public decimal MinLimit { get; set; }
        /// <summary>
        /// ["<c>flexibleMaxLimit</c>"] Max limit
        /// </summary>
        [JsonPropertyName("flexibleMaxLimit")]
        public decimal MaxLimit { get; set; }
    }
}

