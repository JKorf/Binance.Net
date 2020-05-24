using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Candlestick information for symbol
    /// </summary>
    public interface IBinanceFuturesKline
    {
        /// <summary>
        /// The time this candlestick opened
        /// </summary>
        DateTime OpenTime { get; set; }
        /// <summary>
        /// The price at which this candlestick opened
        /// </summary>
        decimal Open { get; set; }
        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        decimal High { get; set; }
        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        decimal Low { get; set; }
        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        decimal Close { get; set; }
        /// <summary>
        /// The volume traded during this candlestick
        /// </summary>
        decimal Volume { get; set; }
        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        DateTime CloseTime { get; set; }
        /// <summary>
        /// The volume traded during this candlestick in the asset form
        /// </summary>
        decimal QuoteAssetVolume { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        int TradeCount { get; set; }
        /// <summary>
        /// Taker buy base asset volume
        /// </summary>
        decimal TakerBuyBaseAssetVolume { get; set; }
        /// <summary>
        /// Taker buy quote asset volume
        /// </summary>
        decimal TakerBuyQuoteAssetVolume { get; set; }
    }
}
