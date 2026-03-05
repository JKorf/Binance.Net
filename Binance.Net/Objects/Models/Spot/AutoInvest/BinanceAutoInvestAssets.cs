namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest assets
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestAssets
    {
        /// <summary>
        /// Supported target assets.
        /// </summary>
        [JsonPropertyName("targetAssets")]
        public string[] TargetAssets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Supported source assets.
        /// </summary>
        [JsonPropertyName("sourceAssets")]
        public string[] SourceAssets { get; set; } = Array.Empty<string>();
    }

}
