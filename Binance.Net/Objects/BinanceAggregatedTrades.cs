using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Compressed aggregated trade information. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
    /// </summary>
    public class BinanceAggregatedTrades
    {
        /// <summary>
        /// The id of this aggregation
        /// </summary>
        [JsonProperty("a")]
        public long AggregateTradeId { get; set; }
        /// <summary>
        /// The price of trades in this aggregation
        /// </summary>
        [JsonProperty("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The total quantity of trades in the aggregation
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
        /// The timestamp of the trades
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Whether the buyer was the maker
        /// </summary>
        [JsonProperty("m")]
        public bool BuyerWasMaker { get; set; }
        /// <summary>
        /// Whether the trade was matched at the best price
        /// </summary>
        [JsonProperty("M")]
        public bool WasBestPriceMatch { get; set; }
    }
}
