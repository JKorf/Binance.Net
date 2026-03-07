using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Book tick
    /// </summary>
    [SerializationModel]
    public record BinanceStreamBookPrice : IBinanceBookPrice
    {
        /// <summary>
        /// ["<c>u</c>"] The update identifier.
        /// </summary>
        [JsonPropertyName("u")]
        public long UpdateId { get; set; }
        /// <summary>
        /// ["<c>s</c>"] The symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>b</c>"] Price of the best bid
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>B</c>"] Quantity of the best bid
        /// </summary>
        [JsonPropertyName("B")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Price of the best ask
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>A</c>"] Quantity of the best ask
        /// </summary>
        [JsonPropertyName("A")]
        public decimal BestAskQuantity { get; set; }
    }
}

