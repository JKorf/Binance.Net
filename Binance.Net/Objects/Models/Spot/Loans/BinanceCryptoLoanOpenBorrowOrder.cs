namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Open borrow order info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanOpenBorrowOrder
    {
        /// <summary>
        /// ["<c>loanCoin</c>"] The loaning asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralCoin</c>"] The collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralAmount</c>"] The collateral quantity
        /// </summary>
        [JsonPropertyName("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// ["<c>totalDebt</c>"] Total debt
        /// </summary>
        [JsonPropertyName("totalDebt")]
        public decimal TotalDebt { get; set; }
        /// <summary>
        /// ["<c>currentLTV</c>"] Current LTV.
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal CurrentLTV { get; set; }
    }
}

