using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageRebate
    {
        [JsonProperty("subaccountId")]
        public string SubAccountId { get; set; }

        [JsonProperty("income")]
        public decimal Income { get; set; }
        
        [JsonProperty("asset")]
        public string Asset { get; set; }
        
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        [JsonProperty("tradeId")]
        public string TradeId { get; set; }

        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Date { get; set; }
    }
}