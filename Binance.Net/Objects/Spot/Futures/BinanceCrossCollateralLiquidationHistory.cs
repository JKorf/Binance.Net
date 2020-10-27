using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Liquidation history
    /// </summary>
    public class BinanceCrossCollateralLiquidationHistory
    {
        /// <summary>
        /// Amount for liquidation
        /// </summary>
        public decimal CollateralAmountForLiquidation { get; set; }

        /// <summary>
        /// Collateral coin
        /// </summary>
        public string CollateralCoin { get; set; } = "";
        /// <summary>
        /// Start time of liquidation
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ForceLiquidationStartTime { get; set; }
        /// <summary>
        /// Coin
        /// </summary>
        public string Coin { get; set; } = "";
        /// <summary>
        /// Rest collateral amount after liquidation
        /// </summary>
        public decimal RestCollateralAmountAfterLiquidation { get; set; }
        /// <summary>
        /// Rest loan amount
        /// </summary>
        public decimal RestLoanAmount { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = "";
    }
}
