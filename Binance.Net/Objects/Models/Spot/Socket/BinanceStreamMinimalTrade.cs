using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Aggregated information about trades for a symbol
    /// </summary>
    public record BinanceStreamMinimalTrade
    {
        /// <summary>
        /// The price of the trades
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The time of the trade
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TradeTime { get; set; }
    }
}
