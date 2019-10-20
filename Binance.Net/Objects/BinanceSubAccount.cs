using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    internal class BinanceSubAccountWrapper
    {
        [JsonProperty("msg")]
        public string? Message { get; set; }
        public bool Success { get; set; }
        public IEnumerable<BinanceSubAccount>? SubAccounts { get; set; }
    }

    /// <summary>
    /// Sub account details
    /// </summary>
    public class BinanceSubAccount
    {
        /// <summary>
        /// The email associated with the sub account
        /// </summary>
        public string Email { get; set; } = "";
        /// <summary>
        /// The status of the sub account
        /// </summary>
        [JsonConverter(typeof(SubAccountStatusConverter))]
        public SubAccountStatus Status { get; set; }
        /// <summary>
        /// Whether or not the sub account has been activated
        /// </summary>
        public bool Activated { get; set; }
        /// <summary>
        /// The mobile associated with the sub account
        /// </summary>
        public string Mobile { get; set; } = "";
        /// <summary>
        /// If Google authentication is enabled
        /// </summary>
        [JsonProperty("gAuth")]
        public bool GoogleAuthentication { get; set; }
        /// <summary>
        /// The time the sub account was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreateTime { get; set; }
    }
}
