﻿using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageSubAccountApiKey
    {
        [JsonProperty("subaccountId")]
        public string Id { get; set; }
        
        [JsonProperty("apikey")]
        public string ApiKey { get; set; }
        
        [JsonProperty("canTrade")]
        public bool IsTradingEnabled { get; set; }
        
        [JsonProperty("marginTrade")]
        public bool IsMarginTradingEnabled { get; set; }
        
        [JsonProperty("futuresTrade")]
        public bool IsFuturesTradingEnabled { get; set; }
    }
}