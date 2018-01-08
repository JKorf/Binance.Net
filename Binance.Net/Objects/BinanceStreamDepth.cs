using Newtonsoft.Json;
using System.Collections.Generic;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Update data about an order book for a symbol
    /// </summary>
    public class BinanceStreamDepth: BinanceStreamEvent
    {
        /// <summary>
        /// The symbol the update is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; }
        /// <summary>
        /// The id of this update, can be synced with <see cref="BinanceClient.GetOrderBook"/> to update the orderbook
        /// </summary>
        [JsonProperty("U")]
        public long FirstUpdateId { get; set; }
        /// <summary>
        /// List of updated bids. If quantity is 0 the entry can be removed
        /// </summary>
        [JsonProperty("u")]
        public long LastUpdateId { get; set; }
        /// <summary>
        /// List of updated bids. If quantity is 0 the entry can be removed
        /// </summary>
        [JsonProperty("b")]
        public List<BinanceOrderBookEntry> Bids { get; set; }
        /// <summary>
        /// List of updated asks. If quantity is 0 the entry can be removed
        /// </summary>
        [JsonProperty("a")]
        public List<BinanceOrderBookEntry> Asks { get; set; }
    }
}
