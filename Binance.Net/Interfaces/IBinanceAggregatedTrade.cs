using System;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Compressed aggregated trade information. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
    /// </summary>
    public interface IBinanceAggregatedTrade
    {
        /// <summary>
        /// The id of this aggregation
        /// </summary>
        long Id { get; set; }
        /// <summary>
        /// The price of trades in this aggregation
        /// </summary>
        decimal Price { get; set; }
        /// <summary>
        /// The total quantity of trades in the aggregation
        /// </summary>
        decimal Quantity { get; set; }
        /// <summary>
        /// The first trade id in this aggregation
        /// </summary>
        long FirstTradeId { get; set; }
        /// <summary>
        /// The last trade id in this aggregation
        /// </summary>
         long LastTradeId { get; set; }
        /// <summary>
        /// The timestamp of the trades
        /// </summary>
        DateTime TradeTime { get; set; }
        /// <summary>
        /// Whether the buyer was the maker
        /// </summary>
        bool BuyerIsMaker { get; set; }
    }
}
