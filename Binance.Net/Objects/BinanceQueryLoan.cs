using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    public class BinanceQueryLoanList
    {
        public BinanceQueryLoan[] Rows { get; set; }
        public int Total { get; set; }
    }

    public class BinanceQueryLoan
    {
        // <summary>
        /// The asset of the loan
        /// </summary>
        public string Asset { get; set; }
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
        /// The status of the loan
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(MarginStatusConverter))]
        public MarginStatus Status { get; set; }
    }
}
