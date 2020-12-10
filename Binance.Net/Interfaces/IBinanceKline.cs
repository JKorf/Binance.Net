using System;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Kline data
    /// </summary>
    public interface IBinanceKline: ICommonKline
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
        decimal BaseVolume { get; set; }

        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        DateTime CloseTime { get; set; }

        /// <summary>
        /// The volume traded during this candlestick in the asset form
        /// </summary>
        decimal QuoteVolume { get; set; }

        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        int TradeCount { get; set; }

        /// <summary>
        /// Taker buy base asset volume
        /// </summary>
        decimal TakerBuyBaseVolume { get; set; }

        /// <summary>
        /// Taker buy quote asset volume
        /// </summary>
        decimal TakerBuyQuoteVolume { get; set; }
    }
}