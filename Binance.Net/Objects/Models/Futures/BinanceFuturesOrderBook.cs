using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesOrderBook : BinanceOrderBook
    {
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string? Pair { get; set; } = string.Empty;
        /// <summary>
        /// The symbol of the order book 
        /// </summary>
        [JsonPropertyName("symbol")]
        public new string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The symbol of the order book 
        /// </summary>
        [JsonPropertyName("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime MessageTime { get; set; }

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }
}
