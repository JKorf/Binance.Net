using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// The result of transfering from main to margin account or margin to main
    /// </summary>
    public class BinanceTransfer
    {
        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        public long TranId { get; set; }
    }
}
