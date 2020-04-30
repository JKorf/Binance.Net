using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageTransferTransaction
    {
        [JsonProperty("txnId")]
        public string Id { get; set; }
        
        [JsonProperty("clientTranId")]
        public string ClientTransferId { get; set; }
        
        [JsonProperty("fromId")]
        public string FromId { get; set; }
        
        [JsonProperty("toId")]
        public string ToId { get; set; }
        
        [JsonProperty("asset")]
        public string Asset { get; set; }
        
        [JsonProperty("qty")]
        public decimal Amount { get; set; }
        
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Date { get; set; }
    }
}