using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceTradingStatusWrapper
    {
        public bool Success { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
        public BinanceTradingStatus Status { get; set; }
    }

    public class BinanceTradingStatus
    {
        public bool IsLocked { get; set; }
        public int PlannedRecoverTime { get; set; }

        [JsonProperty("triggerCondition")]
        public Dictionary<string, int> TriggerConditions { get; set; }

        public Dictionary<string, List<BinanceIndicator>> Indicators { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }

    public class BinanceIndicator
    {
        [JsonProperty("i")]
        public string Indicator { get; set; }
        [JsonProperty("c")]
        public int Count { get; set; }
        [JsonProperty("v")]
        public decimal CurrentValue { get; set; }
        [JsonProperty("t")]
        public decimal TriggerValue { get; set; }
    }
}
