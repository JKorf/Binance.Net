using Binance.Net.Interfaces;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Information about the best price/quantity available for a symbol
    /// </summary>
    public class BinanceFuturesBookPrice : IBinanceBookPrice
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The highest bid price for the symbol
        /// </summary>
        [JsonProperty("bidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// The quantity of the highest bid price currently in the order book
        /// </summary>
        [JsonProperty("bidQty")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// The lowest ask price for the symbol
        /// </summary>
        [JsonProperty("askPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// The quantity of the lowest ask price currently in the order book
        /// </summary>
        [JsonProperty("askQty")]
        public decimal BestAskQuantity { get; set; }
    }
}
