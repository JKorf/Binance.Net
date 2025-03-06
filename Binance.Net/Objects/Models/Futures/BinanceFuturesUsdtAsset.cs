namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesUsdtAsset
    {
        /// <summary>
        /// Name of the asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Whether the asset can be used as margin in Multi-Assets mode
        /// </summary>
        [JsonPropertyName("marginAvailable")]
        public bool MarginAvailable { get; set; }
        /// <summary>
        /// Auto-exchange threshold in Multi-Assets margin mode
        /// </summary>
        [JsonPropertyName("autoAssetExchange")]
        public decimal? AutoAssetExchange { get; set; }
    }
}
