﻿using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketStream
{
    /// <summary>
    /// Wrapper for kline information for a symbol
    /// </summary>
    public class BinanceStreamIndexKlineData : BinanceStreamEvent
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// The data
        /// </summary>
        [JsonProperty("k")]
        public BinanceFuturesStreamIndexKline Data { get; set; } = default!;
    }

    /// <summary>
    /// Index kline
    /// </summary>
    public class BinanceFuturesStreamIndexKline
    {
        /// <summary>
        /// Open time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("t")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Close time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("T")]
        public DateTime CloseTime { get; set; }

        /// <summary>
        /// Ignore
        /// </summary>
        [JsonProperty("s")]
        public string Ignore1 { get; set; } = "";
        /// <summary>
        /// Kline interval
        /// </summary>
        [JsonProperty("i")]
        [JsonConverter(typeof(KlineIntervalConverter))]
        public KlineInterval Interval { get; set; }

        /// <summary>
        /// Ignore
        /// </summary>
        [JsonProperty("f")]
        public string Ignore2 { get; set; } = "";
        /// <summary>
        /// Ignore
        /// </summary>
        [JsonProperty("L")]
        public string Ignore3 { get; set; } = "";

        /// <summary>
        /// Open price of the kline
        /// </summary>
        [JsonProperty("o")]
        public decimal Open { get; set; }
        /// <summary>
        /// Close price of the kline
        /// </summary>
        [JsonProperty("c")]
        public decimal Close { get; set; }
        /// <summary>
        /// High price of the kline
        /// </summary>
        [JsonProperty("h")]
        public decimal High { get; set; }
        /// <summary>
        /// Low price of the kline
        /// </summary>
        [JsonProperty("l")]
        public decimal Low { get; set; }

        /// <summary>
        /// Ignore
        /// </summary>
        [JsonProperty("v")]
        public string Ignore4 { get; set; } = "";
        
        /// <summary>
        /// Number of basic data
        /// </summary>
        [JsonProperty("n")]
        public int NumberOfBasicData { get; set; }

        /// <summary>
        /// Is the kline closed
        /// </summary>
        [JsonProperty("x")]
        public bool Closed { get; set; }

        /// <summary>
        /// Ignore
        /// </summary>
        [JsonProperty("q")]
        public string Ignore5 { get; set; } = "";
        /// <summary>
        /// Ignore
        /// </summary>
        [JsonProperty("V")]
        public string Ignore6 { get; set; } = "";
        /// <summary>
        /// Ignore
        /// </summary>
        [JsonProperty("Q")]
        public string Ignore7 { get; set; } = "";
        /// <summary>
        /// Ignore
        /// </summary>
        [JsonProperty("b")]
        public string Ignore8 { get; set; } = "";
    }
}
