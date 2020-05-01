using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageBnbBurnStatus
    {
        [JsonProperty("subAccountId")]
        public string SubAccountId { get; set; }
        
        [JsonProperty("spotBNBBurn")]
        public bool SpotBnbBurn { get; set; }
        
        [JsonProperty("interestBNBBurn")]
        public bool InterestBnbBurn { get; set; }
    }
}