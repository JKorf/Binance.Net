﻿using Binance.Net.Objects;
using System.Collections.Generic;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public interface IBinanceFuturesOrderBook
    {
        /// <summary>
        /// The symbol of the order book (only filled from stream updates)
        /// </summary>
        string Symbol { get; set; }

        /// <summary>
        /// The ID of the last update
        /// </summary>
        public long LastUpdateId { get; set; }

        /// <summary>
        /// The list of bids
        /// </summary>
        IEnumerable<BinanceFuturesOrderBookEntry> Bids { get; set; }

        /// <summary>
        /// The list of asks
        /// </summary>
        IEnumerable<BinanceFuturesOrderBookEntry> Asks { get; set; }
    }
}
