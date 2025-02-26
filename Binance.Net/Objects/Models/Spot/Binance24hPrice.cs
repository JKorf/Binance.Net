using Binance.Net.Interfaces;
using Binance.Net.Converters;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    public record Binance24HPrice : Binance24HPriceBase, IBinanceTick
    {
        /// <summary>
        /// The close price 24 hours ago
        /// </summary>
        [JsonPropertyName("prevClosePrice")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// The best bid price in the order book
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// The quantity of the best bid price in the order book
        /// </summary>
        [JsonPropertyName("bidQty")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// The best ask price in the order book
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// The quantity of the best ask price in the order book
        /// </summary>
        [JsonPropertyName("askQty")]
        public decimal BestAskQuantity { get; set; }
        
        /// <inheritdoc />
        [JsonPropertyName("volume")]
        public override decimal Volume { get; set; }
        /// <inheritdoc />
        [JsonPropertyName("quoteVolume")]
        public override decimal QuoteVolume { get; set; }
    }
}
