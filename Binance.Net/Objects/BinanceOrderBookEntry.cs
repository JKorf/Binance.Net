using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// An entry in the order book
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class BinanceOrderBookEntry : ISymbolOrderBookEntry
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
