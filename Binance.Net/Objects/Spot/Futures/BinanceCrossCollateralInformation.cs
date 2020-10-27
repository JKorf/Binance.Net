using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Collateral info
    /// </summary>
    public class BinanceCrossCollateralInformation
    {
        /// <summary>
        /// The collateral coin
        /// </summary>
        public string CollateralCoin { get; set; } = "";
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// Margin call collateral rate
        /// </summary>
        public decimal MarginCallCollateralRate { get; set; }
        /// <summary>
        /// Liquidation collateal rate
        /// </summary>
        public decimal LiquidationCollateralRate { get; set; }
        /// <summary>
        /// Current collateral rate
        /// </summary>
        public decimal CurrentCollateralRate { get; set; }
    }
}
