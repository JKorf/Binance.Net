namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Reference price update
    /// </summary>
    public record BinanceReferencePriceUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>r</c>"] Reference price
        /// </summary>
        [JsonPropertyName("r")]
        public decimal ReferencePrice { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }


}
