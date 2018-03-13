using System;
using Newtonsoft.Json;
using Binance.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Aggregated information about trades for a symbol
    /// </summary>
    public class BinanceStreamAggregatedTrade: BinanceStreamEvent
    {
        /// <summary>
        /// The symbol the trade was for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; }
        /// <summary>
        /// The id of this aggregated trade
        /// </summary>
        [JsonProperty("a")]
        public long AggregatedTradeId { get; set; }
        /// <summary>
        /// The price of the trades
        /// </summary>
        [JsonProperty("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The combined quantity of the trades
        /// </summary>
        [JsonProperty("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The first trade id in this aggregation
        /// </summary>
        [JsonProperty("f")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// The last trade id in this aggregation
        /// </summary>
        [JsonProperty("l")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// The time of the trades
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Whether the buyer was the maker
        /// </summary>
        [JsonProperty("m")]
        public bool BuyerIsMaker { get; set; }
        
        [JsonProperty("M")]
        public bool Ignore { get; set; }
    }
}
