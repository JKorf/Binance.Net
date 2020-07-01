using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Api Key
    /// </summary>
    public class BinanceBrokerageSubAccountApiKey
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = "";
        
        /// <summary>
        /// Api Key
        /// </summary>
        public string ApiKey { get; set; } = "";
        
        /// <summary>
        /// Is Trading Enabled
        /// </summary>
        [JsonProperty("canTrade")]
        public bool IsTradingEnabled { get; set; }
        
        /// <summary>
        /// Is Margin Trading Enabled
        /// </summary>
        [JsonProperty("marginTrade")]
        public bool IsMarginTradingEnabled { get; set; }
        
        /// <summary>
        /// Is Futures Trading Enabled
        /// </summary>
        [JsonProperty("futuresTrade")]
        public bool IsFuturesTradingEnabled { get; set; }
    }
}