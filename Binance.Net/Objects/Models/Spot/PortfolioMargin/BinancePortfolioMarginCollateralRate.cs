namespace Binance.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Portfolio margin collateral rate info
    /// </summary>
    [SerializationModel]
    public record BinancePortfolioMarginCollateralRate
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>collateralRate</c>"] Collateral rate
        /// </summary>
        [JsonPropertyName("collateralRate")]
        public decimal CollateralRate { get; set; }

        /// <summary>
        /// ["<c>riskBasedLiquidationRatio</c>"] Risk based liquidation ratio
        /// </summary>
        [JsonPropertyName("riskBasedLiquidationRatio")]
        public decimal RiskBasedLiquidationRatio { get; set; }

        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}

