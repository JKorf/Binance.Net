namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest assets
    /// </summary>
    public record BinanceAutoInvestAssets
    {
        /// <summary>
        /// Target assets
        /// </summary>
        [JsonPropertyName("targetAssets")]
        public string[] TargetAssets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Source assets
        /// </summary>
        [JsonPropertyName("sourceAssets")]
        public string[] SourceAssets { get; set; } = Array.Empty<string>();
    }

}
