using System;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// 24 hour price stats
    /// </summary>
    public interface IBinance24HPrice
    {
        /// <summary>
        /// The symbol the price is for
        /// </summary>
        string Symbol { get; set; }

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
        /// The most recent trade price
        /// </summary>
        decimal LastPrice { get; set; }

        /// <summary>
        /// The most recent trade quantity
        /// </summary>
        decimal LastQuantity { get; set; }

        /// <summary>
        /// The open price 24 hours ago
        /// </summary>
        decimal OpenPrice { get; set; }

        /// <summary>
        /// The highest price in the last 24 hours
        /// </summary>
        decimal HighPrice { get; set; }

        /// <summary>
        /// The lowest price in the last 24 hours
        /// </summary>
        decimal LowPrice { get; set; }

        /// <summary>
        /// The base volume traded in the last 24 hours
        /// </summary>
        decimal BaseVolume { get; set; }

        /// <summary>
        /// The quote asset volume traded in the last 24 hours
        /// </summary>
        decimal QuoteVolume { get; set; }

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