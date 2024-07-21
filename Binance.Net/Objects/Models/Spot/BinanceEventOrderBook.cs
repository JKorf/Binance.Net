using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Stream order book
    /// </summary>
    public record BinanceEventOrderBook: BinanceOrderBook, IBinanceEventOrderBook
    {
        /// <summary>
        /// The id of this update, can be synced with BinanceClient.Spot.GetOrderBook to update the order book
        /// </summary>
        [JsonPropertyName("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// Setter for last update id, need for Json.Net
        /// </summary>
        [JsonPropertyName("u")]
        internal long LastUpdateIdStream { set => LastUpdateId = value; }

        /// <summary>
        /// Event type
        /// </summary>
        [JsonPropertyName("e")]
        internal string EventType { get; set; } = string.Empty;

        /// <summary>
        /// Event time of the update
        /// </summary>
        [JsonPropertyName("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime EventTime { get; set; }
        
        /// <summary>
        /// Setter for bids (needed forJson.Net)
        /// </summary>
        [JsonPropertyName("b")]
        internal IEnumerable<BinanceOrderBookEntry> BidsStream { set => Bids = value; }

        /// <summary>
        /// Setter for asks (needed forJson.Net)
        /// </summary>
        [JsonPropertyName("a")]
        internal IEnumerable<BinanceOrderBookEntry> AsksStream { set => Asks = value; }
    }
}
