using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Tick info
    /// </summary>
    public class BinanceStreamTick: BinanceStreamEvent
    {        
        /// <summary>
        /// The symbol this data is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; }
        /// <summary>
        /// The price change of this symbol
        /// </summary>
        [JsonProperty("p")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// The price change percentage of this symbol
        /// </summary>
        [JsonProperty("P")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// The weighted average
        /// </summary>
        [JsonProperty("w")]
        public decimal WeightedAverage { get; set; }
        /// <summary>
        /// The close price of the previous day
        /// </summary>
        [JsonProperty("x")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// The current day close price. This is the latest price for this symbol.
        /// </summary>
        [JsonProperty("c")]
        public decimal CurrentDayClosePrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Q")]
        public decimal CloseTradesQuantity { get; set; }
        /// <summary>
        /// The best bid price in the order book
        /// </summary>
        [JsonProperty("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// The quantity of the best bid price available
        /// </summary>
        [JsonProperty("B")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// The best ask price in the order book
        /// </summary>
        [JsonProperty("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// The quantity of the best ask price
        /// </summary>
        [JsonProperty("A")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Todays open price
        /// </summary>
        [JsonProperty("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Todays high price
        /// </summary>
        [JsonProperty("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Todays low price
        /// </summary>
        [JsonProperty("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Total traded volume in the base asset
        /// </summary>
        [JsonProperty("v")]
        public decimal TotalTradedBaseAssetVolume { get; set; }
        /// <summary>
        /// Total traded volume in the quote asset
        /// </summary>
        [JsonProperty("q")]
        public decimal TotalTradedQuoteAssetVolume { get; set; }
        /// <summary>
        /// The first trade id of today
        /// </summary>
        [JsonProperty("F")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// The last trade id of today
        /// </summary>
        [JsonProperty("L")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// The total trades of id
        /// </summary>
        [JsonProperty("n")]
        public long TotalTrades { get; set; }
        /// <summary>
        /// The open time of these stats
        /// </summary>
        [JsonProperty("O"), JsonConverter(typeof(TimestampConverter))]
        public DateTime StatisticsOpenTime { get; set; }
        /// <summary>
        /// The close time of these stats
        /// </summary>
        [JsonProperty("C"), JsonConverter(typeof(TimestampConverter))]
        public DateTime StatisticsCloseTime { get; set; }
    }
}
