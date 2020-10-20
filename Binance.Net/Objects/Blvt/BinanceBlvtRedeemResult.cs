using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Blvt
{
    /// <summary>
    /// Redeem result
    /// </summary>
    public class BinanceBlvtRedeemResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(BlvtStatusConverter))]
        public BlvtStatus Status { get; set; }
        /// <summary>
        /// Name of the token
        /// </summary>
        public string TokenName { get; set; } = "";
        /// <summary>
        /// Redemption value in usdt
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Redemption token amount
        /// </summary>
        public decimal RedeemAmount { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
