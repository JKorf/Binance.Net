using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects
{
    public class BinanceStreamDepth
    {
        [JsonProperty("e")]
        public string Event { get; set; }
        [JsonProperty("E"), JsonConverter(typeof(TimestampConverter))]
        public DateTime EventTime { get; set; }
        [JsonProperty("s")]
        public string Symbol { get; set; }
        [JsonProperty("u")]
        public long UpdateId { get; set; }
        [JsonProperty("b")]
        public List<BinanceOrderBookEntry> Bids { get; set; }
        [JsonProperty("a")]
        public List<BinanceOrderBookEntry> Asks { get; set; }
    }
}
