using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Wrapper for kline information for a symbol
    /// </summary>
    public class BinanceStreamKlineData: BinanceStreamEvent
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
        public BinanceStreamKline Data { get; set; }
    }

    /// <summary>
    /// The kline data
    /// </summary>
    public class BinanceStreamKline
    {
        /// <summary>
        /// The open time of this candlestick
        /// </summary>
        [JsonProperty("t"), JsonConverter(typeof(TimestampConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }
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
        /// The highest price of this candlestick
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
        public decimal QuoteAssetVolume { get; set; }
        /// <summary>
        /// The volume of active buy
        /// </summary>
        [JsonProperty("V")]
        public decimal TakerBuyBaseAssetVolume { get; set; }
        /// <summary>
        /// The quote volume of active buy
        /// </summary>
        [JsonProperty("Q")]
        public decimal TakerBuyQuoteAssetVolume { get; set; }

        /// <summary>
        /// Casts this object to a <see cref="BinanceKline"/> object
        /// </summary>
        /// <returns></returns>
        public BinanceKline ToKline()
        {
            return new BinanceKline
            {
                Open = Open,
                Close = Close,
                Volume = Volume,
                CloseTime = CloseTime,
                High = High,
                Low = Low,
                OpenTime = OpenTime,
                QuoteAssetVolume = QuoteAssetVolume,
                TakerBuyBaseAssetVolume = TakerBuyBaseAssetVolume,
                TakerBuyQuoteAssetVolume = TakerBuyQuoteAssetVolume,
                TradeCount = TradeCount
            };
        }
    }
}
