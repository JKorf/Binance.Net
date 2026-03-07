namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesUsdtAsset
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset name.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginAvailable</c>"] Whether the asset can be used as margin in Multi-Assets mode
        /// </summary>
        [JsonPropertyName("marginAvailable")]
        public bool MarginAvailable { get; set; }
        /// <summary>
        /// ["<c>autoAssetExchange</c>"] Auto-exchange threshold in Multi-Assets margin mode
        /// </summary>
        [JsonPropertyName("autoAssetExchange")]
        public decimal? AutoAssetExchange { get; set; }
    }
}

