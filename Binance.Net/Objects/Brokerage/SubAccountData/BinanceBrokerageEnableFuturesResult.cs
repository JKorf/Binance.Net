using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageEnableFuturesResult
    {
        [JsonProperty("subaccountId")]
        public string Id { get; set; }
        
        [JsonProperty("enableFutures")]
        public bool EnableFutures { get; set; }
        
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }
}