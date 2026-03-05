namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Multi asset mode info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesMultiAssetMode
    {
        /// <summary>
        /// Whether multi-asset mode is enabled.
        /// </summary>
        [JsonPropertyName("multiAssetsMargin")]
        public bool MultiAssetMode { get; set; }
    }
}
