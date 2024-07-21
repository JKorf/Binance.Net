namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Ltv adjustment info
    /// </summary>
    public record BinanceCryptoLoanLtvAdjustRecord
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
        public string Direction { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Pre adjust ltv
        /// </summary>
        public decimal PreLtv { get; set; }
        /// <summary>
        /// Post adjust ltv
        /// </summary>
        public decimal AfterLtv { get; set; }
        /// <summary>
        /// Adjust time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime AdjustTime { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
    }
}
