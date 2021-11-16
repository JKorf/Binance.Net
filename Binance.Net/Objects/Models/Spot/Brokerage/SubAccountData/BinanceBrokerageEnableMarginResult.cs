using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Margin Result
    /// </summary>
    public class BinanceBrokerageEnableMarginResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Is Margin Enabled
        /// </summary>
        [JsonProperty("enableMargin")]
        public bool IsMarginEnabled { get; set; }
        
        /// <summary>
        /// Update Date
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateDate { get; set; }
    }
}