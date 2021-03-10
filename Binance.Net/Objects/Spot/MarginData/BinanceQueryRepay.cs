using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// Repay info
    /// </summary>
    public class BinanceRepay
    {
        /// <summary>
        /// The asset of the repay
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// The transaction id of the repay
        /// </summary>`
        [JsonProperty("txId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Total amount repaid
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Interest repaid
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// Principal repaid
        /// </summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Time of repay completed
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        /// <summary>
        /// The status of the repay
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(MarginStatusConverter))]
        public MarginStatus Status { get; set; }
    }
}
