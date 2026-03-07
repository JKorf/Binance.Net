using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about the best price/quantity available for a symbol
    /// </summary>
    [SerializationModel]
    public record BinanceBookPrice : IBinanceBookPrice
    {
        /// <summary>
        /// ["<c>lastUpdateId</c>"] Last trade update id
        /// </summary>
        [JsonPropertyName("lastUpdateId")]
        public long LastUpdateId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bidPrice</c>"] The highest bid price for the symbol
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bidQty</c>"] The quantity of the highest bid price currently in the order book
        /// </summary>
        [JsonPropertyName("bidQty")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>askPrice</c>"] The lowest ask price for the symbol
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>askQty</c>"] The quantity of the lowest ask price currently in the order book
        /// </summary>
        [JsonPropertyName("askQty")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The data timestamp.
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}

