using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// The result amount of geting maxBorrowable or maxTransferable 
    /// </summary>
    public class BinanceMarginAmount
    {
        /// <summary>
        /// The amount
        /// </summary>
        public decimal Amount { get; set; }
    }
}
