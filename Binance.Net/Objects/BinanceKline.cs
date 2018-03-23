using System;
using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
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
        public decimal Open { get; set; }
        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        public decimal High { get; set; }
        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        public decimal Close { get; set; }
        /// <summary>
        /// The volume traded during this candlestick
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// The volume traded during this candlestick in the asset form
        /// </summary>
        public decimal AssetVolume { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        public int Trades { get; set; }
        /// <summary>
        /// Taker buy base asset volume
        /// </summary>
        public decimal TakerBuyBaseAssetVolume { get; set; }
        /// <summary>
        /// Taker buy quote asset volume
        /// </summary>
        public decimal TakerBuyQuoteAssetVolume { get; set; }
    }
}
