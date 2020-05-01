using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Create Result
    /// </summary>
    public class BinanceBrokerageSubAccountCreateResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonProperty("subaccountId")]
        public string SubAccountId { get; set; } = "";
    }
}