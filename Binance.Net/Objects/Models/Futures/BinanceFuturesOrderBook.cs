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
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string? Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] The symbol of the order book 
        /// </summary>
        [JsonPropertyName("symbol")]
        public new string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>E</c>"] The symbol of the order book 
        /// </summary>
        [JsonPropertyName("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime MessageTime { get; set; }

        /// <summary>
        /// ["<c>T</c>"] The ID of the last update
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }
}

