namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Price index for a symbol
    /// </summary>
    public record BinanceMarginPriceIndex
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Time of calculation
        /// </summary>
        [JsonPropertyName("calcTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CalculationTime { get; set; }
    }
}
