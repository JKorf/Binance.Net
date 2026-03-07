using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamOrderBookDepth : BinanceStreamEvent, IBinanceFuturesEventOrderBook
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol of the order book (only filled from stream updates)
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>T</c>"] The time the event happened
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }

        /// <summary>
        /// ["<c>U</c>"] The ID of the first update
        /// </summary>
        [JsonPropertyName("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// ["<c>u</c>"] The ID of the last update
        /// </summary>
        [JsonPropertyName("u")]
        public long LastUpdateId { get; set; }


        /// <summary>
        /// ["<c>pu</c>"] The ID of the last update Id in last stream
        /// </summary>
        [JsonPropertyName("pu")]
        public long LastUpdateIdStream { get; set; }


        /// <summary>
        /// ["<c>b</c>"] The list of diff bids
        /// </summary>
        [JsonPropertyName("b")]
        public BinanceOrderBookEntry[] Bids { get; set; } = Array.Empty<BinanceOrderBookEntry>();

        /// <summary>
        /// ["<c>a</c>"] The list of diff asks
        /// </summary>
        [JsonPropertyName("a")]
        public BinanceOrderBookEntry[] Asks { get; set; } = Array.Empty<BinanceOrderBookEntry>();
    }
}

