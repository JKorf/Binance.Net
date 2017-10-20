using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects
{

    public class BinanceDepositList
    {
        [JsonProperty("depositList")]
        public List<BinanceDeposit> List { get; set; }
        public bool Success { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
    }

    public class BinanceDeposit
    {
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime InsertTime { get; set; }
        public double Amount { get; set; }
        public string Asset { get; set; }
        public bool Status { get; set; }
    }
}
