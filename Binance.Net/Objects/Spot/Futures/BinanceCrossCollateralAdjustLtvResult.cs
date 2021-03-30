using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Adjust result
    /// </summary>
    public class BinanceCrossCollateralAdjustLtvResult
    {
        /// <summary>
        /// Collateral coin
        /// </summary>
        public string CollateralCoin { get; set; } = "";
        /// <summary>
        /// Loan coin
        /// </summary>
        public string LoanCoin { get; set; } = "";
        /// <summary>
        /// The direction
        /// </summary>
        [JsonConverter(typeof(AdjustRateDirectionConverter))]
        public AdjustRateDirection Direction { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }
}
