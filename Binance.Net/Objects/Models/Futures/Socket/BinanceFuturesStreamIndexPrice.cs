using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Index price update
    /// </summary>
    public class BinanceFuturesStreamIndexPrice: BinanceStreamEvent
    {
        /// <summary>
        /// The pair
        /// </summary>
        [JsonProperty("i")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// The index price
        /// </summary>
        [JsonProperty("p")]
        public decimal IndexPrice { get; set; }
    }
}
