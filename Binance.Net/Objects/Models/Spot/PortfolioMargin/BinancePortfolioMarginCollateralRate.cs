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

        /// <summary>
        /// ["<c>tieredLiquidationRatio</c>"] Tiered liquidation ratio
        /// </summary>
        [JsonPropertyName("tieredLiquidationRatio")]
        public BinanceTieredLiquidationRatio[]? TieredLiquidationRatio { get; set; }
    }

    /// <summary>
    /// Tiered liquidation ratio
    /// </summary>
    public record BinanceTieredLiquidationRatio
    {
        /// <summary>
        /// ["<c>tierFloor</c>"] Tier floor
        /// </summary>
        [JsonPropertyName("tierFloor")]
        public decimal TierFloor { get; set; }
        /// <summary>
        /// ["<c>tierCap</c>"] Tier cap
        /// </summary>
        [JsonPropertyName("tierCap")]
        public decimal TierCap { get; set; }
        /// <summary>
        /// ["<c>liquidationRatio</c>"] Liquidation ratio
        /// </summary>
        [JsonPropertyName("liquidationRatio")]
        public decimal LiquidationRatio { get; set; }
        /// <summary>
        /// ["<c>cum</c>"] Cum
        /// </summary>
        [JsonPropertyName("cum")]
        public decimal Cum { get; set; }

    }
}

