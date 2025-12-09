using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public record BinanceFuturesStreamMinimalBookUpdate
    {
        /// <summary>
        /// The list of diff bids
        /// </summary>
        [JsonPropertyName("b")]
        public BinanceOrderBookEntry[] Bids { get; set; } = Array.Empty<BinanceOrderBookEntry>();

        /// <summary>
        /// The list of diff asks
        /// </summary>
        [JsonPropertyName("a")]
        public BinanceOrderBookEntry[] Asks { get; set; } = Array.Empty<BinanceOrderBookEntry>();
    }
}
