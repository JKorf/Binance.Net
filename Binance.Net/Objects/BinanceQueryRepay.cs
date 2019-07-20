using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    public class BinanceQueryRepayList
    {
        // <summary>
        /// The list records
        /// </summary>
        public BinanceQueryRepay[] Rows { get; set; }
        // <summary>
        /// The total count of the records
        /// </summary>
        public int Total { get; set; }
    }

    public class BinanceQueryRepay
    {
        // <summary>
        /// The asset of the repay
        /// </summary>
        public string Asset { get; set; }
        /// <summary>
        /// The transaction id of the repay
        /// <summary>`
        [JsonProperty("txId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Total amount repaid
        /// <summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Principal repaid
        /// <summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Time of repay compleated
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
