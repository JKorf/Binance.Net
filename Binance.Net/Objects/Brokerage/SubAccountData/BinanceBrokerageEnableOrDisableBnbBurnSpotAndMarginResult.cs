using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageEnableOrDisableBnbBurnSpotAndMarginResult
    {
        [JsonProperty("subaccountId")]
        public string Id { get; set; }
        
        [JsonProperty("spotBNBBurn")]
        public bool SpotBnbBurn { get; set; }
    }
}