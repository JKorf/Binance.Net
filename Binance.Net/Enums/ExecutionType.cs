using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The type of execution
    /// </summary>
    public enum ExecutionType
    {
        /// <summary>
        /// New
        /// </summary>
        New,
        /// <summary>
        /// Canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// Replaced
        /// </summary>
        Replaced,
        /// <summary>
        /// Rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// Trade
        /// </summary>
        Trade,
        /// <summary>
        /// Expired
        /// </summary>
        Expired
    }
}
