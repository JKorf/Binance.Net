using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The reason the order was rejected
    /// </summary>
    public enum OrderRejectReason
    {
        /// <summary>
        /// Not rejected
        /// </summary>
        None,
        /// <summary>
        /// Unknown instrument
        /// </summary>
        UnknownInstrument,
        /// <summary>
        /// Closed market
        /// </summary>
        MarketClosed,
        /// <summary>
        /// Quantity out of bounds
        /// </summary>
        PriceQuantityExceedsHardLimits,
        /// <summary>
        /// Unknown order
        /// </summary>
        UnknownOrder,
        /// <summary>
        /// Duplicate
        /// </summary>
        DuplicateOrder,
        /// <summary>
        /// Unkown account
        /// </summary>
        UnknownAccount,
        /// <summary>
        /// Not enough balance
        /// </summary>
        InsufficientBalance,
        /// <summary>
        /// Account not active
        /// </summary>
        AccountInactive,
        /// <summary>
        /// Cannot settle
        /// </summary>
        AccountCannotSettle
    }
}
