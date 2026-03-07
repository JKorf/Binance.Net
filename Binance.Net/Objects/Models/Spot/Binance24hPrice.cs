using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    [SerializationModel]
    public record Binance24HPrice : Binance24HPriceBase, IBinanceTick
    {
        /// <summary>
        /// ["<c>prevClosePrice</c>"] The close price 24 hours ago.
        /// </summary>
        [JsonPropertyName("prevClosePrice")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// ["<c>bidPrice</c>"] The best bid price in the order book
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bidQty</c>"] The quantity of the best bid price in the order book
        /// </summary>
        [JsonPropertyName("bidQty")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>askPrice</c>"] The best ask price in the order book
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>askQty</c>"] The quantity of the best ask price in the order book
        /// </summary>
        [JsonPropertyName("askQty")]
        public decimal BestAskQuantity { get; set; }

        /// ["<c>volume</c>"] <inheritdoc />
        [JsonPropertyName("volume")]
        public override decimal Volume { get; set; }
        /// ["<c>quoteVolume</c>"] <inheritdoc />
        [JsonPropertyName("quoteVolume")]
        public override decimal QuoteVolume { get; set; }
    }
}

