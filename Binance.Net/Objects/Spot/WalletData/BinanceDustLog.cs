﻿using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.WalletData
{
    /// <summary>
    /// Dust log response details
    /// </summary>
    public class BinanceDustLogList
    {
        /// <summary>
        /// Total counts of exchange
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// Rows
        /// </summary>
        public IEnumerable<BinanceDustLog> UserAssetDribblets { get; set; } = Array.Empty<BinanceDustLog>();
    }

    /// <summary>
    /// Dust log details
    /// </summary>
    public class BinanceDustLog
    {
        /// <summary>
        /// Total transferred
        /// </summary>
        [JsonProperty("totalTransferedAmount")]
        public decimal TransferredTotal { get; set; }
        /// <summary>
        /// Total service charge
        /// </summary>
        [JsonProperty("totalServiceChargeAmount")]
        public decimal ServiceChargeTotal { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("transId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Detail logs
        /// </summary>
        [JsonProperty("userAssetDribbletDetails")]
        public IEnumerable<BinanceDustLogDetails> Logs { get; set; } = Array.Empty<BinanceDustLogDetails>();
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("operateTime")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime OperateTime { get; set; }
    }

    /// <summary>
    /// Dust log entry details
    /// </summary>
    public class BinanceDustLogDetails
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("transId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Service charge
        /// </summary>
        [JsonProperty("serviceChargeAmount")]
        public decimal ServiceChargeAmount { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("operateTime")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// Transferred amount
        /// </summary>
        [JsonProperty("transferedAmount")]
        public decimal TransferredAmount { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
    }
}
