using System;
using Binance.Net.Converters;
using Newtonsoft.Json;

namespace BinanceAPI.Objects
{
    public class BinanceCheckTime
    {
        [JsonProperty("serverTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime ServerTime { get; set; }
    }
}
