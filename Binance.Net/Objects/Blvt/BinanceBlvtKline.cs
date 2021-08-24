﻿using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Blvt
{
    /// <summary>
    /// Blvt kline
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class BinanceBlvtKline
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
        public decimal Open { get; set; }
        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        [ArrayProperty(2)]
        public decimal High { get; set; }
        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        [ArrayProperty(3)]
        public decimal Low { get; set; }
        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        [ArrayProperty(4)]
        public decimal Close { get; set; }

        /// <summary>
        /// Real leverage
        /// </summary>
        [ArrayProperty(5)]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// The time this candlestick closed
        /// </summary>
        [ArrayProperty(6), JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }

        [ArrayProperty(7)] internal string Ignore { get; set; } = string.Empty;

        /// <summary>
        /// Number of updates
        /// </summary>
        [ArrayProperty(8)]
        public int NavUpdates { get; set; }
    }
}
