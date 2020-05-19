using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    /// <summary>
    /// Sub account position risk
    /// </summary>
    public class BinanceSubAccountFuturesPositionRisk
    {
        /// <summary>
        /// The entry price
        /// </summary>
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        public decimal Leverage { get; set; }
        /// <summary>
        /// Max notional
        /// </summary>
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Position amount
        /// </summary>
        public decimal PositionAmount { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Unrealized profit
        /// </summary>
        public decimal UnrealizedProfit { get; set; }
    }
}
