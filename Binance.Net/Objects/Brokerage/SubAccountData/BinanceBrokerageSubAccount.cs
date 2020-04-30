using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageSubAccount : BinanceBrokerageSubAccountCommission
    {
        [JsonProperty("createTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreateTime { get; set; }
    }
}