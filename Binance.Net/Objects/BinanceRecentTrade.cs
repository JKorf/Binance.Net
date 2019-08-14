using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Recent trade info
    /// </summary>
    public class BinanceRecentTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        /// <summary>
        /// Whether the buyer is maker
        /// </summary>
        public bool IsBuyerMaker { get; set; }
        /// <summary>
        /// Whether the trade was made at the best match
        /// </summary>
        public bool IsBestMatch { get; set; }
    }
}
