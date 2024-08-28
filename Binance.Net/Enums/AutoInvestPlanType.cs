using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Plan type
    /// </summary>
    public enum AutoInvestPlanType
    {
        /// <summary>
        /// Single
        /// </summary>
        [Map("SINGLE")]
        Single,
        /// <summary>
        /// Index
        /// </summary>
        [Map("INDEX")]
        Index,
        /// <summary>
        /// Portfolio
        /// </summary>
        [Map("PORTFOLIO")]
        Portfolio,
        /// <summary>
        /// All
        /// </summary>
        [Map("ALL")]
        All
    }
}
