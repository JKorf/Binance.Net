using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Repay result
    /// </summary>
    public class BinanceCrossCollateralRepayResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public string RepayId { get; set; }
        /// <summary>
        /// The coin borrowed
        /// </summary>
        public string Coin { get; set; } = "";
        /// <summary>
        /// The coin used for collateral
        /// </summary>
        public string CollateralCoin { get; set; } = "";
        /// <summary>
        /// The amount borrowed
        /// </summary>
        public decimal Amount { get; set; }
    }
}
