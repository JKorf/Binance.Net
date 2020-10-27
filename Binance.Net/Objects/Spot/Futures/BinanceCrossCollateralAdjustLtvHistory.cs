using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Adjust history
    /// </summary>
    public class BinanceCrossCollateralAdjustLtvHistory
    {
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Collateral coin
        /// </summary>
        public string CollateralCoin { get; set; } = "";

        /// <summary>
        /// Coin
        /// </summary>
        public string Coin { get; set; } = "";
        /// <summary>
        /// Pre adjustment rate
        /// </summary>
        public decimal PreCollateralRate { get; set; }
        /// <summary>
        /// After adjustment rate
        /// </summary>
        public decimal AfterCollateralRate { get; set; }
        /// <summary>
        /// Direction
        /// </summary>
        [JsonConverter(typeof(AdjustRateDirectionConverter))]
        public AdjustRateDirection Direction { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = "";
        /// <summary>
        /// Time of adjustment
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime AdjustTime { get; set; }
    }
}
