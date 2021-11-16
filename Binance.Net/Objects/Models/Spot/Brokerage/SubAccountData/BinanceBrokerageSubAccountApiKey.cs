using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Api Key
    /// </summary>
    public class BinanceBrokerageSubAccountApiKey
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Api Key
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
        
        /// <summary>
        /// Is Spot Trading Enabled
        /// </summary>
        [JsonProperty("canTrade")]
        public bool IsSpotTradingEnabled { get; set; }
        
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