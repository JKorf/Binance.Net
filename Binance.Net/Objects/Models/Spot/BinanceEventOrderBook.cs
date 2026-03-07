using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Stream order book
    /// </summary>
    [SerializationModel]
    public record BinanceEventOrderBook : IBinanceEventOrderBook
    {
        /// <summary>
        /// ["<c>U</c>"] The id of this update, can be synced with BinanceClient.Spot.GetOrderBook to update the order book
        /// </summary>
        [JsonPropertyName("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// ["<c>u</c>"] Setter for last update id, need for Json.Net
        /// </summary>
        [JsonPropertyName("u")]
        public long LastUpdateId { get; set; }

        /// <summary>
        /// ["<c>s</c>"] The symbol of the order book 
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>e</c>"] Event type
        /// </summary>
        [JsonPropertyName("e")]
        internal string EventType { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>E</c>"] Event time of the update
        /// </summary>
        [JsonPropertyName("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime EventTime { get; set; }

        /// <summary>
        /// ["<c>b</c>"] The list of bids
        /// </summary>
        [JsonPropertyName("b")]
        public BinanceOrderBookEntry[] Bids { get; set; } = Array.Empty<BinanceOrderBookEntry>();
        /// <summary>
        /// ["<c>a</c>"] The list of asks
        /// </summary>
        [JsonPropertyName("a")]
        public BinanceOrderBookEntry[] Asks { get; set; } = Array.Empty<BinanceOrderBookEntry>();
    }
}

