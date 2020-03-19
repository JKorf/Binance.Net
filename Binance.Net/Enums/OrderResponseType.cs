using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Response type
    /// </summary>
    public enum OrderResponseType
    {
        /// <summary>
        /// Ack only
        /// </summary>
        Acknowledge,
        /// <summary>
        /// Resulting order
        /// </summary>
        Result,
        /// <summary>
        /// Full order info
        /// </summary>
        Full
    }
}
