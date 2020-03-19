using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Rate limit on what unit
    /// </summary>
    public enum RateLimitInterval
    {
        /// <summary>
        /// Seconds
        /// </summary>
        Second,
        /// <summary>
        /// Minutes
        /// </summary>
        Minute,
        /// <summary>
        /// Days
        /// </summary>
        Day
    }
}
