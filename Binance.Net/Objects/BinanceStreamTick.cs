using Binance.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    public class BinanceStreamTick
    {
        [JsonProperty("s")]
        public string Symbol { get; set; }
        [JsonProperty("p")]
        public decimal PriceChange { get; set; }
        [JsonProperty("P")]
        public decimal PriceChangePercentage { get; set; }
        [JsonProperty("w")]
        public decimal WeightedAverage { get; set; }
        [JsonProperty("x")]
        public decimal PrevDayClosePrice { get; set; }
        [JsonProperty("c")]
        public decimal CurrentDayClosePrice { get; set; }
        [JsonProperty("Q")]
        public decimal CloseTradesQuantity { get; set; }
        [JsonProperty("b")]
        public decimal BestBidPrice { get; set; }
        [JsonProperty("B")]
        public decimal BestBidQuantity { get; set; }
        [JsonProperty("a")]
        public decimal BestAskPrice { get; set; }
        [JsonProperty("A")]
        public decimal BestAskQuantity { get; set; }
        [JsonProperty("o")]
        public decimal OpenPrice { get; set; }
        [JsonProperty("h")]
        public decimal HighPrice { get; set; }
        [JsonProperty("l")]
        public decimal LowPrice { get; set; }
        [JsonProperty("v")]
        public decimal TotalTradedBaseAssetVolume { get; set; }
        [JsonProperty("q")]
        public decimal TotalTradedQuoteAssetVolume { get; set; }
        [JsonProperty("F")]
        public long FirstTradeId { get; set; }
        [JsonProperty("L")]
        public long LastTradeId { get; set; }
        [JsonProperty("n")]
        public long TotalTrades { get; set; }
        [JsonProperty("O"), JsonConverter(typeof(TimestampConverter))]
        public DateTime StatisticsOpenTime { get; set; }
        [JsonProperty("C"), JsonConverter(typeof(TimestampConverter))]
        public DateTime StatisticsCloseTime { get; set; }
    }
}
