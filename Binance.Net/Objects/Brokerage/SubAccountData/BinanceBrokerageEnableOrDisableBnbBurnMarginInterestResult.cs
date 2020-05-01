using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Or Disable BNB Burn Margin Interest Result
    /// </summary>
    public class BinanceBrokerageEnableOrDisableBnbBurnMarginInterestResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonProperty("subaccountId")]
        public string SubAccountId { get; set; } = "";
        
        /// <summary>
        /// Is Interest BNB Burn
        /// </summary>
        [JsonProperty("interestBNBBurn")]
        public bool IsInterestBnbBurn { get; set; }
    }
}