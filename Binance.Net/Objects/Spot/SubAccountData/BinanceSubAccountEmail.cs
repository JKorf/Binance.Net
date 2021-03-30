using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    /// <summary>
    /// Sub account details
    /// </summary>
    public class BinanceSubAccountEmail
    {
        /// <summary>
        /// The email associated with the sub account
        /// </summary>
        public string Email { get; set; } = "";        
    }
}
