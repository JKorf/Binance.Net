namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Max quantities
    /// </summary>
    public record BinanceCrossCollateralAdjustMaxAmounts
    {
        /// <summary>
        /// The max in amount
        /// </summary>
        [JsonPropertyName("maxInAmount")]
        public decimal MaxInQuantity { get; set; }
        /// <summary>
        /// The max out amount
        /// </summary>
        [JsonPropertyName("maxOutAmount")]
        public decimal MaxOutQuantity { get; set; }
    }
}
