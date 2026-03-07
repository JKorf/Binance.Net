using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Aggregated information about trades for a symbol
    /// </summary>
    [SerializationModel]
    public record BinanceStreamTrade : BinanceStreamEvent, IBinanceTrade
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol the trade was for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>t</c>"] The id of this trade
        /// </summary>
        [JsonPropertyName("t")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>p</c>"] The price of the trades
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>q</c>"] The quantity of the trade
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>b</c>"] The buyer order id
        /// </summary>
        [JsonPropertyName("b")]
        public long BuyerOrderId { get; set; }
        /// <summary>
        /// ["<c>a</c>"] The seller order id.
        /// </summary>
        [JsonPropertyName("a")]
        public long SellerOrderId { get; set; }
        /// <summary>
        /// ["<c>T</c>"] The time of the trade
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Whether the buyer was the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }

        /// <summary>
        /// ["<c>M</c>"] Unused
        /// </summary>
        [JsonPropertyName("M")]
        public bool IsBestMatch { get; set; }

        /// <summary>
        /// ["<c>X</c>"] Update type
        /// </summary>
        [JsonPropertyName("X")]
        public string? Type { get; set; }
    }
}

