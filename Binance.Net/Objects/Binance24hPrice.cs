using System;
using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class Binance24hPrice
    {
        public double PriceChange { get; set; }
        public double PriceChangePercent { get; set; }
        [JsonProperty("weightedAvgPrice")]
        public double WeightedAveragePrice { get; set; }
        [JsonProperty("prevClosePrice")]
        public double PreviousClosePrice { get; set; }
        public double LastPrice { get; set; }
        public double BidPrice { get; set; }
        public double AskPrice { get; set; }
        public double OpenPrice { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double Volume { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime OpenTime { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }
        [JsonProperty("fristId")] // ?
        public long FirstId { get; set; }
        public long LastId { get; set; }
        [JsonProperty("count")]
        public int Trades { get; set; }
    }
}
