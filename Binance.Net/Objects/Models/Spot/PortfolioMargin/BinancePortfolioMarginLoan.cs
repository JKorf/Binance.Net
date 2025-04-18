namespace Binance.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Bankruptcy loan info
    /// </summary>
    [SerializationModel]
    public record BinancePortfolioMarginLoan
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Loan amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}
