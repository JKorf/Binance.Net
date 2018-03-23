using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Wrapper for kline information for a symbol
    /// </summary>
    public class BinanceStreamKline: BinanceStreamEvent
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; }
        /// <summary>
        /// The data
        /// </summary>
        [JsonProperty("k")]
        public BinanceStreamKlineInner Data { get; set; }
    }

    /// <summary>
    /// The kline data
    /// </summary>
    public class BinanceStreamKlineInner
    {
        /// <summary>
        /// The start time of this candlestick
        /// </summary>
        [JsonProperty("t"), JsonConverter(typeof(TimestampConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// The end time of this candlestick
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// The symbol this candlestick is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; }
        /// <summary>
        /// The interval of this candlestick
        /// </summary>
        [JsonProperty("i"), JsonConverter(typeof(KlineIntervalConverter))]
        public KlineInterval Interval { get; set; }
        /// <summary>
        /// The first trade id in this candlestick
        /// </summary>
        [JsonProperty("f")]
        public long FirstTrade { get; set; }
        /// <summary>
        /// The last trade id in this candlestick
        /// </summary>
        [JsonProperty("L")]
        public long LastTrade { get; set; }
        /// <summary>
        /// The open price of this candlestick
        /// </summary>
        [JsonProperty("o")]
        public decimal Open { get; set; }
        /// <summary>
        /// The close price of this candlestick
        /// </summary>
        [JsonProperty("c")]
        public decimal Close { get; set; }
        /// <summary>
        /// The higest price of this candlestick
        /// </summary>
        [JsonProperty("h")]
        public decimal High { get; set; }
        /// <summary>
        /// The lowest price of this candlestick
        /// </summary>
        [JsonProperty("l")]
        public decimal Low { get; set; }
        /// <summary>
        /// The volume traded during this candlestick
        /// </summary>
        [JsonProperty("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        [JsonProperty("n")]
        public int TradeCount { get; set; }
        /// <summary>
        /// Boolean indicating whether this candlestick is closed
        /// </summary>
        [JsonProperty("x")]
        public bool Final { get; set; }
        /// <summary>
        /// The quote volume
        /// </summary>
        [JsonProperty("q")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// The volume of active buy
        /// </summary>
        [JsonProperty("V")]
        public decimal ActiveBuyVolume { get; set; }
        /// <summary>
        /// The quote volume of active buy
        /// </summary>
        [JsonProperty("Q")]
        public decimal QuoteActiveBuyVolume { get; set; }
    }
}
