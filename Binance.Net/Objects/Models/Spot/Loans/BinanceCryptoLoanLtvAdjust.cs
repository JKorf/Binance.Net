namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Adjust info
    /// </summary>
    [SerializationModel]
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
        [JsonPropertyName("direction")]
        public string Direction { get; set; } = string.Empty;
        /// <summary>
        /// Adjustment amount
        /// </summary>
        [JsonPropertyName("adjustmentAmount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Current ltv
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal CurrentLtv { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public decimal Status { get; set; }
    }
}
