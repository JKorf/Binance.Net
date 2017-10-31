using System;
using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    public class Binance24HPrice
    {
        /// <summary>
        /// The actuals price change in the last 24 hours
        /// </summary>
        public double PriceChange { get; set; }
        /// <summary>
        /// The price change in percentage in the last 24 hours
        /// </summary>
        public double PriceChangePercent { get; set; }
        /// <summary>
        /// The weighted average price in the last 24 hours
        /// </summary>
        [JsonProperty("weightedAvgPrice")]
        public double WeightedAveragePrice { get; set; }
        /// <summary>
        /// The close price 24 hours ago
        /// </summary>
        [JsonProperty("prevClosePrice")]
        public double PreviousClosePrice { get; set; }
        /// <summary>
        /// The most recent price
        /// </summary>
        public double LastPrice { get; set; }
        /// <summary>
        /// The most recent bid price
        /// </summary>
        public double BidPrice { get; set; }
        /// <summary>
        /// The most recent ask price
        /// </summary>
        public double AskPrice { get; set; }
        /// <summary>
        /// The open price 24 hours ago
        /// </summary>
        public double OpenPrice { get; set; }
        /// <summary>
        /// The highest price in the last 24 hours
        /// </summary>
        public double HighPrice { get; set; }
        /// <summary>
        /// The lowest price in the last 24 hours
        /// </summary>
        public double LowPrice { get; set; }
        /// <summary>
        /// The volume traded in the last 24 hours
        /// </summary>
        public double Volume { get; set; }
        /// <summary>
        /// Time at which this 24 hours opened
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Time at which this 24 hours closed
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// The first trade ID in the last 24 hours
        /// </summary>
        [JsonProperty("fristId")] // ?
        public long FirstId { get; set; }
        /// <summary>
        /// The last trade ID in the last 24 hours
        /// </summary>
        public long LastId { get; set; }
        /// <summary>
        /// The amount of trades made in the last 24 hours
        /// </summary>
        [JsonProperty("count")]
        public int Trades { get; set; }
    }
}
