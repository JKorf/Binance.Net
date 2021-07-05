using Newtonsoft.Json;
using System.Collections.Generic;

namespace Binance.Net.Objects.Spot.WalletData
{
    /// <summary>
    /// Account status info
    /// </summary>
    public class BinanceAccountStatus
    {
        /// <summary>
        /// The result message
        /// </summary>
        [JsonProperty("data")]
        public string? Data { get; set; }
    }
}
