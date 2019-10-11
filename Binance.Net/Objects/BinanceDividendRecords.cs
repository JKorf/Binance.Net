using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Dividend records
    /// </summary>
    public class BinanceDividendRecords
    {
        /// <summary>
        /// Records
        /// </summary>
        public IEnumerable<BinanceDividendRecord> Rows { get; set; } = new List<BinanceDividendRecord>();
        /// <summary>
        /// Total records
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// Dividend record
    /// </summary>
    public class BinanceDividendRecord
    {
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// Timestamp of the transaction
        /// </summary>
        [JsonConverter(typeof(TimestampConverter)), JsonProperty("divTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public string TransactionId { get; set; } = "";
        /// <summary>
        /// Info
        /// </summary>
        [JsonProperty("enInfo")]
        public string? Info { get; set; }
    }
}
