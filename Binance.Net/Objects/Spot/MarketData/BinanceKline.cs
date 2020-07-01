using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarketData
{
    /// <summary>
    /// Candlestick information for symbol
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class BinanceKline
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
        /// The volume traded during this candlestick
        /// </summary>
        [ArrayProperty(5)]
        public decimal Volume { get; set; }
        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [ArrayProperty(6), JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// The volume traded during this candlestick in the asset form
        /// </summary>
        [ArrayProperty(7)]
        public decimal QuoteAssetVolume { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        [ArrayProperty(8)]
        public int TradeCount { get; set; }
        /// <summary>
        /// Taker buy base asset volume
        /// </summary>
        [ArrayProperty(9)]
        public decimal TakerBuyBaseAssetVolume { get; set; }
        /// <summary>
        /// Taker buy quote asset volume
        /// </summary>
        [ArrayProperty(10)]
        public decimal TakerBuyQuoteAssetVolume { get; set; }
    }
}
