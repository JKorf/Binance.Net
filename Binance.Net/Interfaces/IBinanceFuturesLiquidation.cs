using Binance.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Forced liquidation info
    /// </summary>
    public interface IBinanceFuturesLiquidation
    {
        /// <summary>
        /// Symbol
        /// </summary>
        string Symbol { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        decimal Price { get; set; }
        /// <summary>
        /// Total quantity
        /// </summary>
        decimal LastQuantityFilled { get; set; }
        /// <summary>
        /// The executed quantity
        /// </summary>
        decimal QuantityFilled { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        decimal AveragePrice { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        OrderStatus Status { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        OrderSide Side { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        OrderType Type { get; set; }
        /// <summary>
        /// Forced time
        /// </summary>
        DateTime Time { get; set; }
    }
}
