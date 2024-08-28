using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Subscription cycle
    /// </summary>
    public enum AutoInvestSubscriptionCycle
    {
        /// <summary>
        /// One hour
        /// </summary>
        [Map("H1")]
        OneHour,
        /// <summary>
        /// Four hour
        /// </summary>
        [Map("H4")]
        FourHour,
        /// <summary>
        /// Eight hour
        /// </summary>
        [Map("H8")]
        EightHour,
        /// <summary>
        /// Twelve hour
        /// </summary>
        [Map("H12")]
        TwelveHour,
        /// <summary>
        /// Daily
        /// </summary>
        [Map("DAILY")]
        Daily,
        /// <summary>
        /// Weekly
        /// </summary>
        [Map("WEEKLY")]
        Weekly,
        /// <summary>
        /// Bi-Weekly
        /// </summary>
        [Map("BI_WEEKLY")]
        BiWeekly,
        /// <summary>
        /// Monthly
        /// </summary>
        [Map("MONTHLY")]
        Monthly
    }
}
