namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Multi asset mode info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesMultiAssetMode
    {
        /// <summary>
        /// ["<c>multiAssetsMargin</c>"] Whether multi-asset mode is enabled.
        /// </summary>
        [JsonPropertyName("multiAssetsMargin")]
        public bool MultiAssetMode { get; set; }
    }
}

