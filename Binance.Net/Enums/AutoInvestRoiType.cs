using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Auto invest ROI type
    /// </summary>
    public enum AutoInvestRoiType
    {
        /// <summary>
        /// Seven days
        /// </summary>
        [Map("SEVEN_DAY")]
        SevenDay,
        /// <summary>
        /// Three months
        /// </summary>
        [Map("THREE_MONTH")]
        ThreeMonth,
        /// <summary>
        /// Six months
        /// </summary>
        [Map("SIX_MONTH")]
        SixMonth,
        /// <summary>
        /// One year
        /// </summary>
        [Map("ONE_YEAR")]
        OneYear,
        /// <summary>
        /// Three year
        /// </summary>
        [Map("THREE_YEAR")]
        ThreeYear,
        /// <summary>
        /// Five year
        /// </summary>
        [Map("FIVE_YEAR")]
        FiveYear
    }
}
