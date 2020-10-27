using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Borrow result
    /// </summary>
    public class BinanceCrossCollateralBorrowResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public string BorrowId { get; set; } = "";
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
        /// <summary>
        /// The collateral amount
        /// </summary>
        public decimal CollateralAmount { get; set; }
        /// <summary>
        /// The timestamp
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
