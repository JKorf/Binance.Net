namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert Pairs
    /// </summary>
    public class BinanceConvertAssetPair
    {
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonProperty("fromAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonProperty("toAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset min quantity
        /// </summary>
        [JsonProperty("fromAssetMinAmount")]
        public decimal QuoteAssetMinQuantity { get; set; }
        /// <summary>
        /// Quote asset max quantity
        /// </summary>
        [JsonProperty("fromAssetMaxAmount")]
        public decimal QuoteAssetMaxQuantity { get; set; }
        /// <summary>
        /// Base asset min quantity
        /// </summary>
        [JsonProperty("toAssetMinAmount")]
        public decimal BaseAssetMinQuantity { get; set; }
        /// <summary>
        /// Base asset max quantity
        /// </summary>
        [JsonProperty("toAssetMaxAmount")]
        public decimal BaseAssetMaxQuantity { get; set; }
    }
}
