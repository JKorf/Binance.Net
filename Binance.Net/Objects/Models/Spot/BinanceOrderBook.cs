using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    [SerializationModel]
    public record BinanceOrderBook : IBinanceOrderBook
    {
        /// <summary>
        /// The symbol of the order book 
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonPropertyName("lastUpdateId")]
        public long LastUpdateId { get; set; }

        /// <summary>
        /// The list of bids
        /// </summary>
        [JsonPropertyName("bids")]
        public BinanceOrderBookEntry[] Bids { get; set; } = Array.Empty<BinanceOrderBookEntry>();

        /// <summary>
        /// The list of asks
        /// </summary>
        [JsonPropertyName("asks")]
        public BinanceOrderBookEntry[] Asks { get; set; } = Array.Empty<BinanceOrderBookEntry>();
    }
}
