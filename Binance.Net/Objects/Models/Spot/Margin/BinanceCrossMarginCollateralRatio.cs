namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Cross margin collateral info
    /// </summary>
    [SerializationModel]
    public record BinanceCrossMarginCollateralRatio
    {
        /// <summary>
        /// Collaterals
        /// </summary>
        [JsonPropertyName("collaterals")]
        public BinanceCrossMarginCollateral[] Collaterals { get; set; } = Array.Empty<BinanceCrossMarginCollateral>();
        /// <summary>
        /// Asset names
        /// </summary>
        [JsonPropertyName("assetNames")]
        public string[] AssetNames { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// Collateral info
    /// </summary>
    public record BinanceCrossMarginCollateral
    {
        /// <summary>
        /// Min usd value
        /// </summary>
        [JsonPropertyName("minUsdValue")]
        public decimal MinUsdValue { get; set; }
        /// <summary>
        /// Max usd value
        /// </summary>
        [JsonPropertyName("maxUsdValue")]
        public decimal? MaxUsdValue { get; set; }
        /// <summary>
        /// Discount rate
        /// </summary>
        [JsonPropertyName("discountRate")]
        public decimal DiscountRate { get; set; }
    }
}
