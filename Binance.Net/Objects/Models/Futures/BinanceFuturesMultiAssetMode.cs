namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Multi asset mode info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesMultiAssetMode
    {
        /// <summary>
        /// Is multi assets mode enabled
        /// </summary>
        [JsonPropertyName("multiAssetsMargin")]
        public bool MultiAssetMode { get; set; }
    }
}
