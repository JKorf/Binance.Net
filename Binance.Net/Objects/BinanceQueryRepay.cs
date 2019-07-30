using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    public class BinanceQueryRepay
    {
        /// <summary>
        /// The asset of the repay
        /// </summary>
        public string Asset { get; set; }
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
