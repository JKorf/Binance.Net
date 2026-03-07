namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Price index for a symbol
    /// </summary>
    [SerializationModel]
    public record BinanceMarginPriceIndex
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>calcTime</c>"] Time of calculation
        /// </summary>
        [JsonPropertyName("calcTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CalculationTime { get; set; }
    }
}

