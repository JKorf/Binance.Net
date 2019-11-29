﻿using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
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
