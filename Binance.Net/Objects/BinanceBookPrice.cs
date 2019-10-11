using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Information about the best price/quantity available for a symbol
    /// </summary>
    public class BinanceBookPrice
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The highest bid price for the symbol
        /// </summary>
        public decimal BidPrice { get; set; }
        /// <summary>
        /// The quantity of the highest bid price currently in the order book
        /// </summary>
        [JsonProperty("bidQty")]
        public decimal BidQuantity { get; set; }
        /// <summary>
        /// The lowest ask price for the symbol
        /// </summary>
        public decimal AskPrice { get; set; }
        /// <summary>
        /// The quantity of the lowest ask price currently in the order book
        /// </summary>
        [JsonProperty("askQty")]
        public decimal AskQuantity { get; set; }
    }
}
