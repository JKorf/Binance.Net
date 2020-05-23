﻿namespace Binance.Net.Enums
{
    /// <summary>
    /// The status of an orderн
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Order is new
        /// </summary>
        New,
        /// <summary>
        /// Order is partly filled, still has quantity left to fill
        /// </summary>
        PartiallyFilled,
        /// <summary>
        /// The order has been filled and completed
        /// </summary>
        Filled,
        /// <summary>
        /// The order has been canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// The order is in the process of being canceled  (currently unused)
        /// </summary>
        PendingCancel,
        /// <summary>
        /// The order has been rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// The order has expired
        /// </summary>
        Expired,
        /// <summary>
        /// Liquidation with Insurance Fund
        /// </summary>
        Insurance,
        /// <summary>
        /// Counterparty Liquidation
        /// </summary>
        Adl
    }
}
