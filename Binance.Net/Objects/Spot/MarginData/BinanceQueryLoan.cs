using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// Loan info
    /// </summary>
    public class BinanceLoan
    {
        /// <summary>
        /// The asset of the loan
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// The transaction id of the loan
        /// </summary>
        [JsonProperty("txId")]
        public long TransactionId { get; set; }
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
        /// The status of the loan
        /// </summary>
        [JsonConverter(typeof(MarginStatusConverter))]
        public MarginStatus Status { get; set; }
    }
}
