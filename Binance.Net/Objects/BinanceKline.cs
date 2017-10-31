using System;
using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Candlestick information for symbol
    /// </summary>
    [JsonConverter(typeof(KlineConverter))]
    public class BinanceKline
    {
        /// <summary>
        /// The time this candlestick opened
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The price at which this candlestick opened
        /// </summary>
        public double Open { get; set; }
        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        public double High { get; set; }
        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        public double Low { get; set; }
        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        public double Close { get; set; }
        /// <summary>
        /// The volume traded during this candlestick
        /// </summary>
        public double Volume { get; set; }
        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// The volume traded during this candlestick in the asset form
        /// </summary>
        public double AssetVolume { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        public int Trades { get; set; }
        /// <summary>
        /// Taker buy base asset volume
        /// </summary>
        public double TakerBuyBaseAssetVolume { get; set; }
        /// <summary>
        /// Taker buy quote asset volume
        /// </summary>
        public double TakerBuyQuoteAssetVolume { get; set; }
    }
}
