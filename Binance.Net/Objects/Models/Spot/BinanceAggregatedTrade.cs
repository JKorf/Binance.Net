using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Compressed aggregated trade information. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
    /// </summary>
    [SerializationModel]
    public record BinanceAggregatedTrade : IBinanceAggregatedTrade
    {
        /// <summary>
        /// ["<c>a</c>"] The id of this aggregation
        /// </summary>
        [JsonPropertyName("a")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>p</c>"] The price of trades in this aggregation
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>q</c>"] The total quantity of trades in the aggregation
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>f</c>"] The first trade id in this aggregation
        /// </summary>
        [JsonPropertyName("f")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// ["<c>l</c>"] The last trade id in this aggregation
        /// </summary>
        [JsonPropertyName("l")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// ["<c>T</c>"] The trade timestamp.
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Whether the buyer was the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// ["<c>M</c>"] Whether the trade was matched at the best price
        /// </summary>
        [JsonPropertyName("M")]
        public bool WasBestPriceMatch { get; set; }
    }
}

