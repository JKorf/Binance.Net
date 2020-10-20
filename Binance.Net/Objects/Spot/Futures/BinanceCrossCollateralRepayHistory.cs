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
    /// Repay history
    /// </summary>
    public class BinanceCrossCollateralRepayHistory
    {
        /// <summary>
        /// Id
        /// </summary>
        public string RepayId { get; set; }
        /// <summary>
        /// Time of confirmation
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ConfirmedTime { get; set; }

        /// <summary>
        /// Time of last update
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Coin
        /// </summary>
        public string Coin { get; set; } = "";
        /// <summary>
        /// Collateral coin
        /// </summary>
        public string CollateralCoin { get; set; } = "";
        /// <summary>
        /// Collateral amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Released collateral amount
        /// </summary>
        public decimal ReleasedCollateral { get; set; }
        /// <summary>
        /// The status of the transfer
        /// </summary>
        [JsonConverter(typeof(FuturesTransferStatusConverter))]
        public FuturesTransferStatus Status { get; set; }
    }
}
