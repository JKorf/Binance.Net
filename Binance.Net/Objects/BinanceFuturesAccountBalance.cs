using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceFuturesAccountBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string? Asset { get; set; }
        /// <summary>
        /// The total balance of this asset
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// The total balance available for withdraw for this asset
        /// </summary>
        public decimal WithdrawAvailable { get; set; }
    }

}
