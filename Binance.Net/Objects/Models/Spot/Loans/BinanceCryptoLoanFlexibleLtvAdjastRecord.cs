namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Ltv adjustment info
    /// </summary>
    public record BinanceCryptoLoanFlexibleLtvAdjustRecord
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
        [JsonPropertyName("direction")]
        public string Direction { get; set; } = string.Empty;
        /// <summary>
        /// Collateral amount
        /// </summary>
        [JsonPropertyName("collateralAmount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// LTV before adjustment.
        /// </summary>
        [JsonPropertyName("preLTV")]
        public decimal PreLtv { get; set; }
        /// <summary>
        /// LTV after adjustment.
        /// </summary>
        [JsonPropertyName("afterLTV")]
        public decimal AfterLtv { get; set; }
        /// <summary>
        /// The adjustment time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("adjustTime")]
        public DateTime AdjustTime { get; set; }
    }
}
