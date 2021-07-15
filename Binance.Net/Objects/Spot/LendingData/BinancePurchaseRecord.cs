using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Purchase record
    /// </summary>
    public class BinancePurchaseRecord
    {
        /// <summary>
        /// Amount purchased
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Lending type
        /// </summary>
        [JsonConverter(typeof(LendingTypeConverter))]
        public LendingType LendingType { get; set; }
        /// <summary>
        /// Name of the product
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// Purchase id
        /// </summary>
        public string PurchaseId { get; set; } = string.Empty;

        /// <summary>
        /// Purchase status
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
