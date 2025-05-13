namespace Binance.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Portfolio margin collateral rate info
    /// </summary>
    [SerializationModel]
    public record BinancePortfolioMarginCollateralRate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Collateral rate
        /// </summary>
        [JsonPropertyName("collateralRate")]
        public decimal CollateralRate { get; set; }
    }
}
