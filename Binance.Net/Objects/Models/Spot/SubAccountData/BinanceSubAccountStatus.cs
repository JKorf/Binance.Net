using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
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
        public string Email { get; set; } = string.Empty;

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
        [JsonProperty("insertTime"), JsonConverter(typeof(DateTimeConverter))]
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
        public bool IsFutureEnabled { get; set; }

        /// <summary>
        /// User mobile number
        /// </summary>
        [JsonProperty("mobile")]
        public string? MobileNumber { get; set; }
    }
}
