using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    /// <summary>
    /// Sub-account Status on Margin/Futures
    /// </summary>
    public class BinanceSubAccountStatus
    {
        /// <summary>
        /// User email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Sub account user enabled
        /// </summary>
        [JsonProperty("isSubUserEnabled")]
        public bool IsAccountEnabled { get; set; }

        /// <summary>
        /// Sub account user active
        /// </summary>
        [JsonProperty("isUserActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// The time the sub account was created
        /// </summary>
        [JsonProperty("insertTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Is Margin enabled
        /// </summary>
        [JsonProperty("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }

        /// <summary>
        /// Is Futures enabled
        /// </summary>
        [JsonProperty("isFutureEnabled")]
        public bool isFutureEnabled { get; set; }

        /// <summary>
        /// User mobile number
        /// </summary>
        [JsonProperty("mobile")]
        public long MobileNumber { get; set; }
    }
}
