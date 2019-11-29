﻿using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
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
