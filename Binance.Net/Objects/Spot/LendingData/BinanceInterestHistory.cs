using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Interest record
    /// </summary>
    public class BinanceLendingInterestHistory
    {
        /// <summary>
        /// Interest
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        /// <summary>
        /// Lending type
        /// </summary>
        [JsonConverter(typeof(LendingTypeConverter))]
        public LendingType LendingType { get; set; }
        /// <summary>
        /// Name of the product
        /// </summary>
        public string ProductName { get; set; } = "";
    }
}
