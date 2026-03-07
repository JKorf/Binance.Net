namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Ltv adjustment info
    /// </summary>
    public record BinanceCryptoLoanFlexibleLtvAdjustRecord
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
        /// ["<c>direction</c>"] Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public string Direction { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralAmount</c>"] Collateral amount
        /// </summary>
        [JsonPropertyName("collateralAmount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>preLTV</c>"] LTV before adjustment.
        /// </summary>
        [JsonPropertyName("preLTV")]
        public decimal PreLtv { get; set; }
        /// <summary>
        /// ["<c>afterLTV</c>"] LTV after adjustment.
        /// </summary>
        [JsonPropertyName("afterLTV")]
        public decimal AfterLtv { get; set; }
        /// <summary>
        /// ["<c>adjustTime</c>"] The adjustment time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("adjustTime")]
        public DateTime AdjustTime { get; set; }
    }
}

