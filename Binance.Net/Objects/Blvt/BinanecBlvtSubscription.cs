using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Blvt
{
    /// <summary>
    /// Leveraged token subscription info
    /// </summary>
    public class BinanecBlvtSubscription
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Token name
        /// </summary>
        public string TokenName { get; set; } = "";
        /// <summary>
        /// Subscription amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// NAV price of subscription
        /// </summary>
        public decimal Nav { get; set; }
        /// <summary>
        /// Subscription fee in usdt
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Subscription cost in usdt
        /// </summary>
        public decimal TotalCharge { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
