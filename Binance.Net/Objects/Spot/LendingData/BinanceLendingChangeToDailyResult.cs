using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Purchase result
    /// </summary>
    public class BinanceLendingChangeToDailyResult
    {
        /// <summary>
        /// The id of the purchase
        /// </summary>
        public int DailyPurchaseId { get; set; }
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
