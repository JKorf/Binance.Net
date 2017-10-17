using System;
using Newtonsoft.Json;
using Binance.Net.Converters;

namespace Binance.Net.Objects
{
    public class BinanceStreamTrade: BinanceStreamEvent
    {
        [JsonProperty("s")]
        public string Symbol { get; set; }
        [JsonProperty("a")]
        public long AggregatedTradeId { get; set; }
        [JsonProperty("p")]
        public double Price { get; set; }
        [JsonProperty("q")]
        public double Quantity { get; set; }
        [JsonProperty("f")]
        public long FirstTradeId { get; set; }
        [JsonProperty("l")]
        public long LastTradeId { get; set; }
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime TradeTime { get; set; }
        [JsonProperty("m")]
        public bool BuyerIsMaker { get; set; }
    }
}
