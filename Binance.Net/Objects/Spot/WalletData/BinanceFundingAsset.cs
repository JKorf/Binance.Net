using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.WalletData
{
    /// <summary>
    /// Funding wallet asset
    /// </summary>
    public class BinanceFundingAsset
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity available
        /// </summary>
        [JsonProperty("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// Quantity locked
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// Quantity frozen
        /// </summary>
        public decimal Freeze { get; set; }
        /// <summary>
        /// Quantity withdrawing
        /// </summary>
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// Value in btc
        /// </summary>
        public decimal BtcValuation { get; set; }
    }
}
