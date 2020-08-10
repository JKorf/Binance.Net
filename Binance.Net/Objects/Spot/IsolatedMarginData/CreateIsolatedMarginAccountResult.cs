using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Spot.IsolatedMarginData
{
    /// <summary>
    /// Result of creating isolated margin account
    /// </summary>
    public class CreateIsolatedMarginAccountResult
    {
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = "";
    }
}
