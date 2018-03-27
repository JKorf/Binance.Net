using System.Collections.Generic;
using Newtonsoft.Json;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public class BinanceOrderBook
    {
        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonProperty("lastUpdateId")]
        public long LastUpdateId { get; set; }
        /// <summary>
        /// The list of bids
        /// </summary>
        public List<BinanceOrderBookEntry> Bids { get; set; }
        /// <summary>
        /// The list of asks
        /// </summary>
        public List<BinanceOrderBookEntry> Asks { get; set; }
    }

    /// <summary>
    /// An entry in the orderbook
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class BinanceOrderBookEntry
    {
        /// <summary>
        /// The price of this order book entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of this price in the order book
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
