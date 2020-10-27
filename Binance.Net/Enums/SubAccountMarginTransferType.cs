using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Sub account margin transfer type
    /// </summary>
    public enum SubAccountMarginTransferType
    {
        /// <summary>
        /// Sub account spot to sub account margin
        /// </summary>
        FromSubAccountSpotToSubAccountMargin,
        /// <summary>
        /// From sub account margin to sub account spot
        /// </summary>
        FromSubAccountMarginToSubAccountSpot
    }
}
