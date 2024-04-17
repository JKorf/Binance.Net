namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Repay result
    /// </summary>
    public class BinanceCrossCollateralRepayResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public string RepayId { get; set; } = string.Empty;
        /// <summary>
        /// The asset borrowed
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The asset used for collateral
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity borrowed
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }
}
