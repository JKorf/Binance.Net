using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public class BinanceFuturesOrderBook : IBinanceFuturesOrderBook
    {
        /// <summary>
        /// The symbol of the order book (only filled from stream updates)
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
        /// The id of this update, can be synced with <see cref="BinanceFuturesClient.GetOrderBook"/> to update the order book
        /// </summary>
        [JsonProperty("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// The list of bids
        /// </summary>
        public IEnumerable<BinanceFuturesOrderBookEntry> Bids { get; set; } = new List<BinanceFuturesOrderBookEntry>();

        /// <summary>
        /// Setter for bids (needed forJson.Net)
        /// </summary>
        [JsonProperty("b")]
        public IEnumerable<BinanceFuturesOrderBookEntry> BidsStream { set => Bids = value; }
        /// <summary>
        /// The list of asks
        /// </summary>
        public IEnumerable<BinanceFuturesOrderBookEntry> Asks { get; set; } = new List<BinanceFuturesOrderBookEntry>();

        /// <summary>
        /// Setter for asks (needed forJson.Net)
        /// </summary>
        [JsonProperty("a")]
        public IEnumerable<BinanceFuturesOrderBookEntry> AsksStream { set => Asks = value; }
    }
}
