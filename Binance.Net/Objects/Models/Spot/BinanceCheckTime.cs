using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarketData
{
    internal class BinanceCheckTime
    {
        [JsonProperty("serverTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime ServerTime { get; set; }
    }
}
