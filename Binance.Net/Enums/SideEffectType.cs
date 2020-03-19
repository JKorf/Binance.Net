using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Side effect for a margin order
    /// </summary>
    public enum SideEffectType
    {
        /// <summary>
        /// Normal trade
        /// </summary>
        NoSideEffect,
        /// <summary>
        /// Margin trade order
        /// </summary>
        MarginBuy,
        /// <summary>
        /// Make auto repayment after order is filled
        /// </summary>
        AutoRepay
    }
}
