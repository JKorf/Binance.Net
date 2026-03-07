namespace Binance.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Bankruptcy loan info
    /// </summary>
    [SerializationModel]
    public record BinancePortfolioMarginLoan
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Loan amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}

