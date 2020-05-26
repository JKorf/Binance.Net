using System;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarketStream
{
    /// <summary>
    /// Book tick
    /// </summary>
    public class BinanceStreamBookPrice : IBinanceBookPrice
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("E"), JsonConverter(typeof(TimestampConverter))]
        public DateTime? EventTime { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime? TransactionTime { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        [JsonProperty("u")]
        public long UpdateId { get; set; }
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Price of the best bid
        /// </summary>
        [JsonProperty("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Quantity of the best bid
        /// </summary>
        [JsonProperty("B")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Price of the best ask
        /// </summary>
        [JsonProperty("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Quantity of the best ask
        /// </summary>
        [JsonProperty("A")]
        public decimal BestAskQuantity { get; set; }
    }
}
