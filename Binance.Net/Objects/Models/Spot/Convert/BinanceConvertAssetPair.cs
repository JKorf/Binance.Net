namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert Pairs
    /// </summary>
    [SerializationModel]
    public record BinanceConvertAssetPair
    {
        /// <summary>
        /// ["<c>fromAsset</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toAsset</c>"] Base asset
        /// </summary>
        [JsonPropertyName("toAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromAssetMinAmount</c>"] Quote asset min quantity
        /// </summary>
        [JsonPropertyName("fromAssetMinAmount")]
        public decimal QuoteAssetMinQuantity { get; set; }
        /// <summary>
        /// ["<c>fromAssetMaxAmount</c>"] Quote asset max quantity
        /// </summary>
        [JsonPropertyName("fromAssetMaxAmount")]
        public decimal QuoteAssetMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>toAssetMinAmount</c>"] Base asset min quantity
        /// </summary>
        [JsonPropertyName("toAssetMinAmount")]
        public decimal BaseAssetMinQuantity { get; set; }
        /// <summary>
        /// ["<c>toAssetMaxAmount</c>"] Base asset max quantity
        /// </summary>
        [JsonPropertyName("toAssetMaxAmount")]
        public decimal BaseAssetMaxQuantity { get; set; }
    }
}

