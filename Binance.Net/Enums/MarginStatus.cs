using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a margin action
    /// </summary>
    public enum MarginStatus
    {
        /// <summary>
        /// Pending to execution
        /// </summary>
        Pending,
        /// <summary>
        /// Executed, waiting to be confirmed
        /// </summary>
        Completed,
        /// <summary>
        /// Successfully loaned/repayed
        /// </summary>
        Confirmed,
        /// <summary>
        /// execution failed, nothing happened to your account
        /// </summary>
        Failed
    }
}
