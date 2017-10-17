using Binance.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    public class BinanceStreamKline: BinanceStreamEvent
    {
        [JsonProperty("s")]
        public string Symbol { get; set; }
        [JsonProperty("k")]
        public BinanceStreamKlineInner Data { get; set; }
    }

    public class BinanceStreamKlineInner
    {
        [JsonProperty("t"), JsonConverter(typeof(TimestampConverter))]
        public DateTime StartTime { get; set; }
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime EndTime { get; set; }
        [JsonProperty("s")]
        public string Symbol { get; set; }
        [JsonProperty("i"), JsonConverter(typeof(KlineIntervalConverter))]
        public KlineInterval Interval { get; set; }
        [JsonProperty("f")]
        public long FirstTrade { get; set; }
        [JsonProperty("L")]
        public long LastTrade { get; set; }
        [JsonProperty("o")]
        public double Open { get; set; }
        [JsonProperty("c")]
        public double Close { get; set; }
        [JsonProperty("h")]
        public double High { get; set; }
        [JsonProperty("l")]
        public double Low { get; set; }
        [JsonProperty("v")]
        public double Volume { get; set; }
        [JsonProperty("n")]
        public int TradeCount { get; set; }
        [JsonProperty("x")]
        public bool Final { get; set; }
        [JsonProperty("q")]
        public double QuoteVolume { get; set; }
        [JsonProperty("V")]
        public double ActiveBuyVolume { get; set; }
        [JsonProperty("Q")]
        public double QuoteActiveBuyVolume { get; set; }
    }


    
}
