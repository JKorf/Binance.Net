using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Or Disable BNB Burn Spot And Margin Result
    /// </summary>
    public class BinanceBrokerageEnableOrDisableBnbBurnSpotAndMarginResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonProperty("subaccountId")]
        public string SubAccountId { get; set; } = "";
        
        /// <summary>
        /// Is Spot BNB Burn
        /// </summary>
        [JsonProperty("spotBNBBurn")]
        public bool IsSpotBnbBurn { get; set; }
    }
}