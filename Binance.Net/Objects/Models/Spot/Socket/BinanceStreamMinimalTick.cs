using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Minimal tick info
    /// </summary>
    public record BinanceStreamMinimalTick
    {
        /// <summary>
        /// ["<c>c</c>"] The current day close price. This is the latest price for this symbol.
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Today's open price.
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] Todays high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Todays low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Total traded volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Total traded quote volume
        /// </summary>
        [JsonPropertyName("q")]
        public decimal QuoteVolume { get; set; }
    }
}

