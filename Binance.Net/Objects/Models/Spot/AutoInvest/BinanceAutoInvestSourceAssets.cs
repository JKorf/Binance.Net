namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest source asset info
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestSourceAssets
    {
        /// <summary>
        /// ["<c>feeRate</c>"] Fee rate
        /// </summary>
        [JsonPropertyName("feeRate")]
        public decimal FeeRate { get; set; }
        /// <summary>
        /// ["<c>taxRate</c>"] Tax rate
        /// </summary>
        [JsonPropertyName("taxRate")]
        public decimal TaxRate { get; set; }
        /// <summary>
        /// ["<c>sourceAssets</c>"] Available source assets.
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
        /// ["<c>sourceAsset</c>"] The source asset.
        /// </summary>
        [JsonPropertyName("sourceAsset")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>assetMinAmount</c>"] Asset min quantity
        /// </summary>
        [JsonPropertyName("assetMinAmount")]
        public decimal AssetMinQuantity { get; set; }
        /// <summary>
        /// ["<c>assetMaxAmount</c>"] Asset max quantity
        /// </summary>
        [JsonPropertyName("assetMaxAmount")]
        public decimal AssetMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>scale</c>"] The quantity precision scale.
        /// </summary>
        [JsonPropertyName("scale")]
        public decimal Scale { get; set; }
        /// <summary>
        /// ["<c>flexibleAmount</c>"] Flexible quantity
        /// </summary>
        [JsonPropertyName("flexibleAmount")]
        public decimal FlexibleQuantity { get; set; }
    }


}

