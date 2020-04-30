using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageEnableMarginResult
    {
        [JsonProperty("subaccountId")]
        public string Id { get; set; }
        
        [JsonProperty("enableMargin")]
        public bool EnableMargin { get; set; }
        
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }
}