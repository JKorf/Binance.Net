using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Book tick
    /// </summary>
    public interface IBinanceBookPrice
    {
        /// <summary>
        /// The symbol
        /// </summary>
        string Symbol { get; set; }
        /// <summary>
        /// Price of the best bid
        /// </summary>
        decimal BestBidPrice { get; set; }
        /// <summary>
        /// Quantity of the best bid
        /// </summary>
        decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Price of the best ask
        /// </summary>
        decimal BestAskPrice { get; set; }
        /// <summary>
        /// Quantity of the best ask
        /// </summary>
        decimal BestAskQuantity { get; set; }
    }
}
