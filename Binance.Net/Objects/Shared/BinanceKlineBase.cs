using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Shared
{
    /// <summary>
    /// Candlestick information for symbol
    /// </summary>
    public abstract class BinanceKlineBase : IBinanceKline
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
        public abstract decimal BaseVolume { get; set; }
        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [ArrayProperty(6), JsonConverter(typeof(TimestampConverter))]
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

        decimal ICommonKline.CommonHigh => High;
        decimal ICommonKline.CommonLow => Low;
        decimal ICommonKline.CommonOpen => Open;
        decimal ICommonKline.CommonClose => Close;
        decimal ICommonKline.CommonVolume => BaseVolume;
        DateTime ICommonKline.CommonOpenTime => OpenTime;
    }
}
