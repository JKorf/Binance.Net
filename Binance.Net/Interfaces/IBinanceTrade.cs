using System;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Information about a trade
    /// </summary>
    public interface IBinanceTrade
    {
        /// <summary>
        /// The order id the trade belongs to
        /// </summary>
        long OrderId { get; set; }
        /// <summary>
        /// The symbol the trade is for
        /// </summary>
        string Symbol { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        decimal Quantity { get; set; }
        /// <summary>
        /// The time the trade was made
        /// </summary>
        DateTime TradeTime { get; set; }
        /// <summary>
        /// Whether account was the buyer in the trade
        /// </summary>
        bool BuyerIsMaker { get; set; }

        /// <summary>
        /// Whether trade was made with the best match
        /// </summary>
        bool IsBestMatch { get; set; }
    }
}
