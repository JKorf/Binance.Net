using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Objects.Spot.MarketStream;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketStream
{
    /// <summary>
    /// Futures book price
    /// </summary>
    public class BinanceFuturesStreamBookPrice : BinanceStreamBookPrice
    {

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime? TransactionTime { get; set; }
        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonProperty("E"), JsonConverter(typeof(TimestampConverter))]
        public DateTime EventTime { get; set; }

        [JsonProperty("e")] private string Event { get; set; } = "";
    }
}
