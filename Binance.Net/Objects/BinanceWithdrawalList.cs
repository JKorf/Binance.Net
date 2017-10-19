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
        public DateTime SuccessTime { get; set; }
        public DateTime ApplyTime { get; set; }
        public double Amount { get; set; }
        public string Address { get; set; }
        [JsonProperty("txId")]
        public string TransactionId { get; set; }
        public string Id { get; set; }
        public string Asset { get; set; }
        public string UserId { get; set; }
        public int Status { get; set; }
    }
}
