using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects
{
    public class BinanceWithdrawalList
    {
        [JsonProperty("withdrawList")]
        public List<BinanceWithdrawal> List { get; set; }
        public bool Success { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
    }

    public class BinanceWithdrawal
    {
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime SuccessTime { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ApplyTime { get; set; }
        public double Amount { get; set; }
        public string Address { get; set; }
        [JsonProperty("txId")]
        public string TransactionId { get; set; }
        public string Asset { get; set; }
        [JsonConverter(typeof(WithdrawalStatusConverter))]
        public WithdrawalStatus Status { get; set; }
    }
}
