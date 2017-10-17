using System;
using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    [JsonConverter(typeof(KlineConverter))]
    public class BinanceKline
    {
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime OpenTime { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }
        public double AssetVolume { get; set; }
        public int Trades { get; set; }
        public double TakerBuyBaseAssetVolume { get; set; }
        public double TakerBuyQuoteAssetVolume { get; set; }
    }
}
