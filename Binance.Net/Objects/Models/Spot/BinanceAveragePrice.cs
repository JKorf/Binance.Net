namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Current average price details for a symbol.
    /// </summary>
    [SerializationModel]
    public record BinanceAveragePrice
    {
        /// <summary>
        /// ["<c>mins</c>"] Averaging duration in minutes.
        /// </summary>
        [JsonPropertyName("mins")]
        public int Minutes { get; set; }
        /// <summary>
        /// ["<c>price</c>"] The average price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>closeTime</c>"] The last trade time
        /// </summary>
        [JsonPropertyName("closeTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime LastTradeTime { get; set; }
    }
}

