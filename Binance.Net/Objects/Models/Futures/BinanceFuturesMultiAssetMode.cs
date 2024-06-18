namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Multi asset mode info
    /// </summary>
    public record BinanceFuturesMultiAssetMode
    {
        /// <summary>
        /// Is multi assets mode enabled
        /// </summary>
        [JsonProperty("multiAssetsMargin")]
        public bool MultiAssetMode { get; set; }
    }
}
