using System;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    internal class BinanceDustLogListWrapper
    {
        public bool Success { get; set; }
        public BinanceDustLogList Results { get; set; }
    }

    internal class BinanceDustLogList
    {
        public int Total { get; set; }
        public BinanceDustLog[] Rows { get; set; }
    }

    /// <summary>
    /// Dust log details
    /// </summary>
    public class BinanceDustLog
    {
        /// <summary>
        /// Total transferred
        /// </summary>
        [JsonProperty("transfered_total")]
        public decimal TransferredTotal { get; set; }
        /// <summary>
        /// Total service charge
        /// </summary>
        [JsonProperty("service_charge_total")]
        public decimal ServiceChargeTotal { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tran_id")]
        public decimal TransactionId { get; set; }
        /// <summary>
        /// Detail logs
        /// </summary>
        [JsonProperty("logs")]
        public BinanceDustLogDetails[] Logs { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("operate_time")]
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
        [JsonProperty("tranId")]
        public decimal TransactionId { get; set; }
        /// <summary>
        /// Service charge
        /// </summary>
        [JsonProperty("serviceChargeAmount")]
        public decimal ServiceChargeAmount { get; set; }
        /// <summary>
        /// Identifier
        /// </summary>
        [JsonProperty("uid")]
        public long UId { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("operateTime")]
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
        public string FromAsset { get; set; }
    }
}
