using System;
using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceAggregatedTrades
    {
        [JsonProperty("a")]
        public int AggregateTradeId { get; set; }
        [JsonProperty("p")]
        public double Price { get; set; }
        [JsonProperty("q")]
        public double Quantity { get; set; }
        [JsonProperty("f")]
        public long FirstTradeId { get; set; }
        [JsonProperty("l")]
        public long LastTradeId { get; set; }
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        [JsonProperty("m")]
        public bool BuyerWasMaker { get; set; }
        [JsonProperty("M")]
        public bool WasBestPriceMatch { get; set; }
    }
}
