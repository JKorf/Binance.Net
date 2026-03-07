using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Symbol configuration
    /// </summary>
    [SerializationModel]
    public record BinanceSymbolConfiguration
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public FuturesMarginType? MarginType { get; set; }
        /// <summary>
        /// ["<c>isAutoAddMargin</c>"] Whether auto add margin is enabled.
        /// </summary>
        [JsonPropertyName("isAutoAddMargin")]
        public bool IsAutoAddMargin { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>maxNotionalValue</c>"] Max notional value
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal MaxNotionalValue { get; set; }
    }


}

