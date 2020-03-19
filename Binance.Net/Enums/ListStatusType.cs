using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// List status type
    /// </summary>
    public enum ListStatusType
    {
        /// <summary>
        /// Failed action
        /// </summary>
        Response,
        /// <summary>
        /// Placed
        /// </summary>
        ExecutionStarted,
        /// <summary>
        /// Order list is done
        /// </summary>
        Done
    }
}
