namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Average price info
    /// </summary>
    public record BinanceStreamAveragePrice : BinanceStreamEvent
    {
        /// <summary>
        /// Duration
        /// </summary>
        [JsonPropertyName("i")]
        public string Interval { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The average price
        /// </summary>
        [JsonPropertyName("w")]
        public decimal Price { get; set; }
        /// <summary>
        /// The last trade time
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime LastTradeTime { get; set; }
    }
}
