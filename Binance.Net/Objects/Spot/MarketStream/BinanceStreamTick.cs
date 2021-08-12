using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;
using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Spot.MarketStream
{
    /// <summary>
    /// Tick info
    /// </summary>
    public abstract class BinanceStreamTickBase: BinanceStreamEvent, IBinanceTick
    {        
        /// <summary>
        /// The symbol this data is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The price change of this symbol
        /// </summary>
        [JsonProperty("p")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// The price change percentage of this symbol
        /// </summary>
        [JsonProperty("P")]
        public decimal PriceChangePercent { get; set; }
        /// <summary>
        /// The weighted average
        /// </summary>
        [JsonProperty("w")]
        public decimal WeightedAveragePrice { get; set; }
        /// <summary>
        /// The close price of the previous day
        /// </summary>
        [JsonProperty("x")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// The current day close price. This is the latest price for this symbol.
        /// </summary>
        [JsonProperty("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// The most recent trade quantity
        /// </summary>
        [JsonProperty("Q")]
        public decimal LastQuantity { get; set; }
        /// <summary>
        /// The best bid price in the order book
        /// </summary>
        [JsonProperty("b")]
        public decimal BidPrice { get; set; }
        /// <summary>
        /// The size of the best bid price in the order book
        /// </summary>
        [JsonProperty("B")]
        public decimal BidQuantity { get; set; }
        /// <summary>
        /// The best ask price in the order book
        /// </summary>
        [JsonProperty("a")]
        public decimal AskPrice { get; set; }
        /// <summary>
        /// The size of the best ask price in the order book
        /// </summary>
        [JsonProperty("A")]
        public decimal AskQuantity { get; set; }
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
        public abstract decimal BaseVolume { get; set; }
        /// <summary>
        /// Total traded volume in the quote asset
        /// </summary>
        public abstract decimal QuoteVolume { get; set; }
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
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The close time of these stats
        /// </summary>
        [JsonProperty("C"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CloseTime { get; set; }
    }

    /// <summary>
    /// Stream tick
    /// </summary>
    public class BinanceStreamTick: BinanceStreamTickBase
    {
        /// <summary>
        /// Total traded volume in the base asset
        /// </summary>
        [JsonProperty("v")]
        public override decimal BaseVolume { get; set; }
        /// <summary>
        /// Total traded volume in the quote asset
        /// </summary>
        [JsonProperty("q")]
        public override decimal QuoteVolume { get; set; }
    }

    /// <summary>
    /// Stream tick
    /// </summary>
    public class BinanceStreamCoinTick : BinanceStreamTickBase
    {
        /// <summary>
        /// Total traded volume in the base asset
        /// </summary>
        [JsonProperty("q")]
        public override decimal BaseVolume { get; set; }
        /// <summary>
        /// Total traded volume in the quote asset
        /// </summary>
        [JsonProperty("v")]
        public override decimal QuoteVolume { get; set; }
    }
}
