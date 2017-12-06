namespace Binance.Net.Objects
{
    /// <summary>
    /// The price of a symbol
    /// </summary>
    public class BinancePrice
    {
        /// <summary>
        /// The symbol the price is for
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The price of the symbol
        /// </summary>
        public decimal Price { get; set; }
    }
}
