using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Information about trades for a symbol
    /// </summary>
    public record BinanceStreamMinimalTrade
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>p</c>"] The price of the trades
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>q</c>"] The quantity of the trade
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>T</c>"] The time of the trade
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TradeTime { get; set; }
    }
}

