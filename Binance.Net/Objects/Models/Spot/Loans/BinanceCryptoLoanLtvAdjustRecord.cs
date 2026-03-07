namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Ltv adjustment info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanLtvAdjustRecord
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
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
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
        /// ["<c>adjustTime</c>"] Adjust time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("adjustTime")]
        public DateTime AdjustTime { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
    }
}

