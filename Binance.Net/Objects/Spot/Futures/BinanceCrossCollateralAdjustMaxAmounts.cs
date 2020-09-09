using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Max amounts
    /// </summary>
    public class BinanceCrossCollateralAdjustMaxAmounts
    {
        /// <summary>
        /// The max in amount
        /// </summary>
        public decimal MaxInAmount { get; set; }
        /// <summary>
        /// The max out amount
        /// </summary>
        public decimal MaxOutAmount { get; set; }
    }
}
