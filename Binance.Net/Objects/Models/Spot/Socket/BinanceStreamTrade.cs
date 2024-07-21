using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Aggregated information about trades for a symbol
    /// </summary>
    public record BinanceStreamTrade: BinanceStreamEvent, IBinanceTrade
    {
        /// <summary>
        /// The symbol the trade was for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The id of this trade
        /// </summary>
        [JsonPropertyName("t")]
        public long Id { get; set; }
        /// <summary>
        /// The price of the trades
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The buyer order id
        /// </summary>
        [JsonPropertyName("b")]
        public long BuyerOrderId { get; set; }
        /// <summary>
        /// The sell order id
        /// </summary>
        [JsonPropertyName("a")]
        public long SellerOrderId { get; set; }
        /// <summary>
        /// The time of the trade
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Whether the buyer was the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        [JsonPropertyName("M")]
        public bool IsBestMatch { get; set; }

        /// <summary>
        /// Update type
        /// </summary>
        [JsonPropertyName("X")]
        public string? Type { get; set; }
    }
}
