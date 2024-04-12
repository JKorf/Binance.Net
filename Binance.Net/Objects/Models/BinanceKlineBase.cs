using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Candlestick information for symbol
    /// </summary>
    public abstract class BinanceKlineBase : IBinanceKline
    {
        /// <summary>
        /// The time this candlestick opened
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
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
        /// <summary>
        /// The volume traded during this candlestick
        /// </summary>
        public abstract decimal Volume { get; set; }
        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [ArrayProperty(6), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// The volume traded during this candlestick in the asset form
        /// </summary>
        public abstract decimal QuoteVolume { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        [ArrayProperty(8)]
        public int TradeCount { get; set; }
        /// <summary>
        /// Taker buy base asset volume
        /// </summary>
        public abstract decimal TakerBuyBaseVolume { get; set; }
        /// <summary>
        /// Taker buy quote asset volume
        /// </summary>
        public abstract decimal TakerBuyQuoteVolume { get; set; }
    }
}
