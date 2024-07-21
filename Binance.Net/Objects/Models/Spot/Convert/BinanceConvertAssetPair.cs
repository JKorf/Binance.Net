namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert Pairs
    /// </summary>
    public record BinanceConvertAssetPair
    {
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("toAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset min quantity
        /// </summary>
        [JsonPropertyName("fromAssetMinAmount")]
        public decimal QuoteAssetMinQuantity { get; set; }
        /// <summary>
        /// Quote asset max quantity
        /// </summary>
        [JsonPropertyName("fromAssetMaxAmount")]
        public decimal QuoteAssetMaxQuantity { get; set; }
        /// <summary>
        /// Base asset min quantity
        /// </summary>
        [JsonPropertyName("toAssetMinAmount")]
        public decimal BaseAssetMinQuantity { get; set; }
        /// <summary>
        /// Base asset max quantity
        /// </summary>
        [JsonPropertyName("toAssetMaxAmount")]
        public decimal BaseAssetMaxQuantity { get; set; }
    }
}
