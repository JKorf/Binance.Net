using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Add IP Restriction Result
    /// </summary>
    public class BinanceBrokerageAddIpRestrictionResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Api key
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateDate { get; set; }
    }
}