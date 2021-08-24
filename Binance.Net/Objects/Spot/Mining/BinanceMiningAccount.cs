using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Mining
{
    /// <summary>
    /// Mining account
    /// </summary>
    public class BinanceMiningAccount
    {
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Hash rates
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<BinanceHashRate> Hashrates { get; set; } = Array.Empty<BinanceHashRate>();
    }
}
