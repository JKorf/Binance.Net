using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Purchase quota left
    /// </summary>
    public class BinancePurchaseQuotaLeft
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = "";

        /// <summary>
        /// The quota left
        /// </summary>
        public decimal LeftQuota { get; set; }
    }
}
