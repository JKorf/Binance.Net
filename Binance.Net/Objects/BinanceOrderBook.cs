using System.Collections.Generic;
using Newtonsoft.Json;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Objects
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public class BinanceOrderBook
    {
        /// <summary>
        /// The symbol the update is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonProperty("lastUpdateId")]
        public long LastUpdateId { get; set; }


        /// <summary>
        /// Setter for last update id, need for Json.Net
        /// </summary>
        [JsonProperty("u")]
        public long LastUpdateIdStream { set => LastUpdateId = value; }

        /// <summary>
        /// The id of this update, can be synced with <see cref="BinanceClient.GetOrderBook"/> to update the order book
        /// </summary>
        [JsonProperty("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// The list of bids
        /// </summary>
        public IEnumerable<BinanceOrderBookEntry> Bids { get; set; } = new List<BinanceOrderBookEntry>();

        /// <summary>
        /// Setter for bids (needed forJson.Net)
        /// </summary>
        [JsonProperty("b")]
        public IEnumerable<BinanceOrderBookEntry> BidsStream { set => Bids = value; }
        /// <summary>
        /// The list of asks
        /// </summary>
        public IEnumerable<BinanceOrderBookEntry> Asks { get; set; } = new List<BinanceOrderBookEntry>();

        /// <summary>
        /// Setter for asks (needed forJson.Net)
        /// </summary>
        [JsonProperty("a")]
        public IEnumerable<BinanceOrderBookEntry> AsksStream { set => Asks = value; }
    }

    /// <summary>
    /// An entry in the order book
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class BinanceOrderBookEntry: ISymbolOrderBookEntry
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
