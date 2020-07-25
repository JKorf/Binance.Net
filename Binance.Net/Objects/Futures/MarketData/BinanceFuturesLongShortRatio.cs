using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Long Short Ratio Info
    /// </summary>
    public class BinanceFuturesLongShortRatio
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = "";

        /// <summary>
        /// long/short ratio
        /// </summary>
        public decimal LongShortRatio { get; set; }

        /// <summary>
        /// longs percentage (in decimal form)
        /// </summary>
        public decimal LongAccount { get; set; }

        /// <summary>
        /// shorts percentage (in decimal form)
        /// </summary>
        public decimal ShortAccount { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime? Timestamp { get; set; }
    }
}