using System;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceDustLogListWrapper
    {
        public bool Success { get; set; }
        public BinanceDustLogList Results { get; set; }
    }

    public class BinanceDustLogList
    {
        public int Total { get; set; }
        public BinanceDustLog[] Rows { get; set; }
    }

    public class BinanceDustLog
    {
        [JsonProperty("transfered_total")]
        public decimal TransferredTotal { get; set; }
        [JsonProperty("service_charge_total")]
        public decimal ServiceChargeTotal { get; set; }
        [JsonProperty("tran_id")]
        public decimal TransactionId { get; set; }
        [JsonProperty("logs")]
        public BinanceDustLogDetails[] Logs { get; set; }
        [JsonProperty("operate_time")]
        public DateTime OperateTime { get; set; }
    }

    public class BinanceDustLogDetails
    {
        [JsonProperty("tranId")]
        public decimal TransactionId { get; set; }
        [JsonProperty("serviceChargeAmount")]
        public decimal ServiceChargeAmount { get; set; }
        [JsonProperty("uid")]
        public long UId { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("operateTime")]
        public DateTime OperateTime { get; set; }
        [JsonProperty("transferedAmount")]
        public decimal TransferredAmount { get; set; }
        [JsonProperty("fromAsset")]
        public string FromAsset { get; set; }
    }
}
