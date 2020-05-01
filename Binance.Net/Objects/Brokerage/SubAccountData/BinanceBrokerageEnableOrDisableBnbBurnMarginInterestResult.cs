using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageEnableOrDisableBnbBurnMarginInterestResult
    {
        [JsonProperty("subaccountId")]
        public string Id { get; set; }
        
        [JsonProperty("interestBNBBurn")]
        public bool InterestBnbBurn { get; set; }
    }
}