namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Current average price details for a symbol.
    /// </summary>
    [SerializationModel]
    public record BinanceAveragePrice
    {
        /// <summary>
        /// Duration in minutes
        /// </summary>
        [JsonPropertyName("mins")]
        public int Minutes { get; set; }
        /// <summary>
        /// The average price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The last trade time
        /// </summary>
        [JsonPropertyName("closeTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime LastTradeTime { get; set; }
    }
}
