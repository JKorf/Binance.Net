namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Repay result
    /// </summary>
    public record BinanceCrossCollateralRepayResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public string RepayId { get; set; } = string.Empty;
        /// <summary>
        /// The asset borrowed
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The asset used for collateral
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity borrowed
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}
