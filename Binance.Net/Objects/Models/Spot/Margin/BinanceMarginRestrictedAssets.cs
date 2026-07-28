namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin restricted assets
    /// </summary>
    [SerializationModel]
    public record BinanceMarginRestrictedAssets
    {
        /// <summary>
        /// ["<c>openLongRestrictedAsset</c>"] Assets which are restricted from opening long positions. These assets can only be sold, not bought
        /// </summary>
        [JsonPropertyName("openLongRestrictedAsset")]
        public string[] OpenLongRestrictedAssets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>maxCollateralExceededAsset</c>"] Assets which have exceeded the maximum collateral limit. These assets can no longer be transfered in
        /// </summary>
        [JsonPropertyName("maxCollateralExceededAsset")]
        public string[] MaxCollateralExceededAssets { get; set; } = Array.Empty<string>();
    }
}
