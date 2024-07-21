using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
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
        public IEnumerable<BinanceOrderBookEntry> Bids { get; set; } = Array.Empty<BinanceOrderBookEntry>();

        /// <summary>
        /// The list of asks
        /// </summary>
        public IEnumerable<BinanceOrderBookEntry> Asks { get; set; } = Array.Empty<BinanceOrderBookEntry>();
    }
}
