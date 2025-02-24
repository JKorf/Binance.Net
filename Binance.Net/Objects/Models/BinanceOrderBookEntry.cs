using Binance.Net.Converters;
using Binance.Net.Objects.Models;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// An entry in the order book
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<BinanceOrderBookEntry, BinanceSourceGenerationContext>))]
    public record BinanceOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price of this order book entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of this price in the order book
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
