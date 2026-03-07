namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Convert symbol info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesConvertSymbol
    {
        /// <summary>
        /// ["<c>fromAsset</c>"] From asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toAsset</c>"] To asset
        /// </summary>
        [JsonPropertyName("toAsset")]
        public string ToAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromAssetMinAmount</c>"] Minimal convert from asset quantity
        /// </summary>
        [JsonPropertyName("fromAssetMinAmount")]
        public decimal FromAssetMinQuantity { get; set; }
        /// <summary>
        /// ["<c>fromAssetMaxAmount</c>"] Maximal convert from asset quantity
        /// </summary>
        [JsonPropertyName("fromAssetMaxAmount")]
        public decimal FromAssetMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>toAssetMinAmount</c>"] Minimal convert to asset quantity
        /// </summary>
        [JsonPropertyName("toAssetMinAmount")]
        public decimal ToAssetMinQuantity { get; set; }
        /// <summary>
        /// ["<c>toAssetMaxAmount</c>"] Maximal convert to asset quantity
        /// </summary>
        [JsonPropertyName("toAssetMaxAmount")]
        public decimal ToAssetMaxQuantity { get; set; }
    }


}

