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
        /// Pre adjust ltv
        /// </summary>
        [JsonPropertyName("preLTV")]
        public decimal PreLtv { get; set; }
        /// <summary>
        /// Post adjust ltv
        /// </summary>
        [JsonPropertyName("afterLTV")]
        public decimal AfterLtv { get; set; }
        /// <summary>
        /// Adjust time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("adjustTime")]
        public DateTime AdjustTime { get; set; }
    }
}
