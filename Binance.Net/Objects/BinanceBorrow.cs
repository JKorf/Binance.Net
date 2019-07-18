using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// The result of borrow asset
    /// </summary>
    public class BinanceBorrow
    {
        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        public long TranId { get; set; }
    }
}
