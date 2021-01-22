using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarketData
{
    /// <summary>
    /// Stream order book
    /// </summary>
    public class BinanceEventOrderBook: BinanceOrderBook, IBinanceEventOrderBook
    {
        /// <summary>
        /// The id of this update, can be synced with BinanceClient.Spot.GetOrderBook to update the order book
        /// </summary>
        [JsonProperty("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// Setter for last update id, need for Json.Net
        /// </summary>
        [JsonProperty("u")]
        internal long LastUpdateIdStream { set => LastUpdateId = value; }

        /// <summary>
        /// Event type
        /// </summary>
        [JsonProperty("e")]
        internal string EventType { get; set; } = "";

        /// <summary>
        /// Event time of the update
        /// </summary>
        [JsonProperty("E"), JsonConverter(typeof(TimestampConverter))]
        public DateTime EventTime { get; set; }
        
        /// <summary>
        /// Setter for bids (needed forJson.Net)
        /// </summary>
        [JsonProperty("b")]
        internal IEnumerable<BinanceOrderBookEntry> BidsStream { set => Bids = value; }

        /// <summary>
        /// Setter for asks (needed forJson.Net)
        /// </summary>
        [JsonProperty("a")]
        internal IEnumerable<BinanceOrderBookEntry> AsksStream { set => Asks = value; }
    }
}
