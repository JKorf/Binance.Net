using Binance.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    public class BinanceStreamEvent
    {
        [JsonProperty("e")]
        public string Event { get; set; }
        [JsonProperty("E"), JsonConverter(typeof(TimestampConverter))]
        public DateTime EventTime { get; set; }
    }
}
