using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Leverage Token Result
    /// </summary>
    public class BinanceBrokerageEnableLeverageTokenResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Is Leverage Token Enabled
        /// </summary>
        [JsonProperty("enableBlvt")]
        public bool IsLeverageTokenEnabled { get; set; }
    }
}