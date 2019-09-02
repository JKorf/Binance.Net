using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Interest history
    /// </summary>
    public class BinanceInterestHistory
    {
        /// <summary>
        /// Total number or rows
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// Results for this page
        /// </summary>
        [JsonProperty("rows")]
        public BinanceInterestHistoryResultEntry[] Data { get; set; }
    }

    /// <summary>
    /// Interest history entry info
    /// </summary>
    public class BinanceInterestHistoryResultEntry
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; }
        /// <summary>
        /// The amount of interest
        /// </summary>
        public decimal InterestAmount { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime InterestAccuredTime { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Type of interest
        /// </summary>
        public string Type { get; set; }
    }
}
