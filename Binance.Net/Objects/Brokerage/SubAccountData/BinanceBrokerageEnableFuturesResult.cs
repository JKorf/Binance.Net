using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Futures Result
    /// </summary>
    public class BinanceBrokerageEnableFuturesResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonProperty("subaccountId")]
        public string SubAccountId { get; set; } = "";
        
        /// <summary>
        /// Is Futures Enabled
        /// </summary>
        [JsonProperty("enableFutures")]
        public bool IsFuturesEnabled { get; set; }
        
        /// <summary>
        /// Update Date
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateDate { get; set; }
    }
}