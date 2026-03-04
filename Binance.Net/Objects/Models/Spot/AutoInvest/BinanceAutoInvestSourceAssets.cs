namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest source asset info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestSourceAssets
    {
        /// <summary>
        /// Fee rate
        /// </summary>
        [JsonPropertyName("feeRate")]
        public decimal FeeRate { get; set; }
        /// <summary>
        /// Tax rate
        /// </summary>
        [JsonPropertyName("taxRate")]
        public decimal TaxRate { get; set; }
        /// <summary>
        /// Available source assets.
        /// </summary>
        [JsonPropertyName("sourceAssets")]
        public BinanceAutoInvestSourceAssetInfo[] SourceAssets { get; set; } = Array.Empty<BinanceAutoInvestSourceAssetInfo>();
    }

    /// <summary>
    /// Source asset constraints and configuration.
    /// </summary>
    public record BinanceAutoInvestSourceAssetInfo
    {
        /// <summary>
        /// The source asset.
        /// </summary>
        [JsonPropertyName("sourceAsset")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// Asset min quantity
        /// </summary>
        [JsonPropertyName("assetMinAmount")]
        public decimal AssetMinQuantity { get; set; }
        /// <summary>
        /// Asset max quantity
        /// </summary>
        [JsonPropertyName("assetMaxAmount")]
        public decimal AssetMaxQuantity { get; set; }
        /// <summary>
        /// The quantity precision scale.
        /// </summary>
        [JsonPropertyName("scale")]
        public decimal Scale { get; set; }
        /// <summary>
        /// Flexible quantity
        /// </summary>
        [JsonPropertyName("flexibleAmount")]
        public decimal FlexibleQuantity { get; set; }
    }


}
