using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Minimal tick info
    /// </summary>
    public record BinanceStreamMinimalCoinTick
    {
        /// <summary>
        /// The current day close price. This is the latest price for this symbol.
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Todays open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Todays high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Todays low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Total traded volume
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Total traded quote volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal QuoteVolume { get; set; }
    }
}
