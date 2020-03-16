using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    public interface IBinanceTick : IBinanceMiniTick
    {
        /// <summary>
        /// The actual price change in the last 24 hours
        /// </summary>
        decimal PriceChange { get; set; }
        /// <summary>
        /// The price change in percentage in the last 24 hours
        /// </summary>
        decimal PriceChangePercent { get; set; }
        /// <summary>
        /// The weighted average price in the last 24 hours
        /// </summary>
        decimal WeightedAveragePrice { get; set; }
        /// <summary>
        /// The close price 24 hours ago
        /// </summary>
         decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// The most recent trade quantity
        /// </summary>
        decimal LastQuantity { get; set; }
        /// <summary>
        /// The best bid price in the order book
        /// </summary>
        decimal BidPrice { get; set; }
        /// <summary>
        /// The size of the best bid price in the order book
        /// </summary>
        decimal BidQuantity { get; set; }
        /// <summary>
        /// The best ask price in the order book
        /// </summary>
        decimal AskPrice { get; set; }
        /// <summary>
        /// The size of the best ask price in the order book
        /// </summary>
        decimal AskQuantity { get; set; }
        /// <summary>
        /// Time at which this 24 hours opened
        /// </summary>
        DateTime OpenTime { get; set; }
        /// <summary>
        /// Time at which this 24 hours closed
        /// </summary>
        DateTime CloseTime { get; set; }
        /// <summary>
        /// The first trade ID in the last 24 hours
        /// </summary>
        long FirstTradeId { get; set; }
        /// <summary>
        /// The last trade ID in the last 24 hours
        /// </summary>
        long LastTradeId { get; set; }
        /// <summary>
        /// The amount of trades made in the last 24 hours
        /// </summary>
        long TotalTrades { get; set; }
    }
}
