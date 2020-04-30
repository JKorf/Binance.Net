using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageTransferResult
    {
        [JsonProperty("txnId")]
        public string Id { get; set; }
        
        [JsonProperty("clientTranId")]
        public string ClientTransferId { get; set; }
    }
}