using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.Blvt
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
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Subscription quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
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
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
