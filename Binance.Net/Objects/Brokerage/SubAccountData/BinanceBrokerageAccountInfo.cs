using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageAccountInfo
    {
        [JsonProperty("maxMakerCommission")]
        public decimal MaxMakerCommission { get; set; }
        
        [JsonProperty("minMakerCommission")]
        public decimal MinMakerCommission { get; set; }
        
        [JsonProperty("maxTakerCommission")]
        public decimal MaxTakerCommission { get; set; }
        
        [JsonProperty("minTakerCommission")]
        public decimal MinTakerCommission { get; set; }
        
        [JsonProperty("subAccountQty")]
        public int SubAccountQty { get; set; }
        
        [JsonProperty("maxSubAccountQty")]
        public int MaxSubAccountQty { get; set; }
    }
}