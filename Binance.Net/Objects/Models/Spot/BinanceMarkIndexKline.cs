using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Kline for mark or index price
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class BinanceMarkIndexKline
    {
        /// <summary>
        /// The time this candlestick opened
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(TimestampConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The price at which this candlestick opened
        /// </summary>
        [ArrayProperty(1)]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        [ArrayProperty(2)]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        [ArrayProperty(3)]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        [ArrayProperty(4)]
        public decimal ClosePrice { get; set; }

        [ArrayProperty(5)] internal string? Ignore1 { get; set; } = string.Empty;
        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [ArrayProperty(6), JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }

        [ArrayProperty(7)] internal string? Ignore2 { get; set; } = string.Empty;
        /// <summary>
        /// Number of basic data
        /// </summary>
        [ArrayProperty(8)]
        public int BasicDataCount { get; set; }
    }
}
