namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Liquidation history
    /// </summary>
    public record BinanceCrossCollateralLiquidationHistory
    {
        /// <summary>
        /// Quantity for liquidation
        /// </summary>
        [JsonPropertyName("collateralAmountForLiquidation")]
        public decimal CollateralQuantityForLiquidation { get; set; }

        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Start time of liquidation
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ForceLiquidationStartTime { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Rest collateral quantity after liquidation
        /// </summary>
        [JsonPropertyName("restCollateralAmountAfterLiquidation")]
        public decimal RestCollateralQuantityAfterLiquidation { get; set; }
        /// <summary>
        /// Rest loan quantity
        /// </summary>
        [JsonPropertyName("restLoanAmount")]
        public decimal RestLoanQuantity { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
