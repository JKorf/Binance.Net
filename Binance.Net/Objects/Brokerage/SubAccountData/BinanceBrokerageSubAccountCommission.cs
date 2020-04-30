﻿using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageSubAccountCommission
    {
        [JsonProperty("subaccountId")]
        public string Id { get; set; }
        
        [JsonProperty("makerCommission")]
        public decimal MakerCommission { get; set; }
        
        [JsonProperty("takerCommission")]
        public decimal TakerCommission { get; set; }
        
        [JsonProperty("marginMakerCommission")]
        public decimal MarginMakerCommission { get; set; }
        
        [JsonProperty("marginTakerCommission")]
        public decimal MarginTakerCommission { get; set; }
    }
}