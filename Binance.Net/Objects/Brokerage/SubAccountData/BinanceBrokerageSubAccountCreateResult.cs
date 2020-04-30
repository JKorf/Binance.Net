using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageSubAccountCreateResult
    {
        [JsonProperty("subaccountId")]
        public string Id { get; set; }
    }
}