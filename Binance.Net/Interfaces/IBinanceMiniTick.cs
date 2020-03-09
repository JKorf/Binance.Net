using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// MiniTick info
    /// </summary>
    public interface IBinanceMiniTick
    {
        /// <summary>
        /// Symbol
        /// </summary>
        string Symbol { get; set; }

        /// <summary>
        /// Close Price
        /// </summary>
        decimal LastPrice { get; set; }

        /// <summary>
        /// Open Price
        /// </summary>
        decimal OpenPrice { get; set; }

        /// <summary>
        /// High
        /// </summary>
        decimal HighPrice { get; set; }

        /// <summary>
        /// Low
        /// </summary>
        decimal LowPrice { get; set; }

        /// <summary>
        /// Total traded base asset volume
        /// </summary>
        decimal TotalTradedBaseAssetVolume { get; set; }

        /// <summary>
        /// Total traded quote asset volume
        /// </summary>
        decimal TotalTradedQuoteAssetVolume { get; set; }
    }
}
