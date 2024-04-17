using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Aggregated information about trades for a symbol
    /// </summary>
    public class BinanceStreamTrade: BinanceStreamEvent, IBinanceTrade
    {
        /// <summary>
        /// The symbol the trade was for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The id of this trade
        /// </summary>
        [JsonProperty("t")]
        public long Id { get; set; }
        /// <summary>
        /// The price of the trades
        /// </summary>
        [JsonProperty("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonProperty("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The buyer order id
        /// </summary>
        [JsonProperty("b")]
        public long BuyerOrderId { get; set; }
        /// <summary>
        /// The sell order id
        /// </summary>
        [JsonProperty("a")]
        public long SellerOrderId { get; set; }
        /// <summary>
        /// The time of the trade
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Whether the buyer was the maker
        /// </summary>
        [JsonProperty("m")]
        public bool BuyerIsMaker { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        [JsonProperty("M")]
        public bool IsBestMatch { get; set; }

        /// <summary>
        /// Update type
        /// </summary>
        [JsonProperty("X")]
        public string? Type { get; set; }
    }
}
