using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Other
{
    /// <summary>
    /// IP restriction info
    /// </summary>
    public class BinanceIpRestriction
    {
        /// <summary>
        /// Is currently restricted
        /// </summary>
        [JsonProperty("ipRestrict")]
        public bool IpRestricted { get; set; }
        /// <summary>
        /// Ip whitelist
        /// </summary>
        public IEnumerable<string> IpList { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Update Time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The API key
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}
