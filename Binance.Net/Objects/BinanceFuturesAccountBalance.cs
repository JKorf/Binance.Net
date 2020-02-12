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
        public string Asset { get; set; }
        public decimal Balance { get; set; }
        public decimal WithdrawAvailable { get; set; }
    }

}
