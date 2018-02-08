using Binance.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    public class BinanceRecentTrade
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        public bool IsBuyerMaker { get; set; }
        public bool IsBestMatch { get; set; }
    }
}
