using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple earn account info
    /// </summary>
    public class BinanceSimpleEarnAccount
    {
        /// <summary>
        /// Total quantity in BTC
        /// </summary>
        [JsonProperty("totalAmountInBTC")]
        public decimal TotalQuantityInBtc { get; set; }
        /// <summary>
        /// Total quantity in USDT
        /// </summary>
        [JsonProperty("totalAmountInUSDT")]
        public decimal TotalQuantityInUsdt { get; set; }
        /// <summary>
        /// Total quantity in BTC in flexible products
        /// </summary>
        [JsonProperty("totalFlexibleAmountInBTC")]
        public decimal TotalFlexibleQuantityInBtc { get; set; }
        /// <summary>
        /// Total quantity in USDT in flexible products
        /// </summary>
        [JsonProperty("totalFlexibleAmountInUSDT")]
        public decimal TotalFlexibleQuantityInUsdt { get; set; }
        /// <summary>
        /// Total quantity in BTC in locked products
        /// </summary>
        [JsonProperty("totalLockedInBTC")]
        public decimal TotalLockedInBtc { get; set; }
        /// <summary>
        /// Total quantity in USDT in locked products
        /// </summary>
        [JsonProperty("totalLockedInUSDT")]
        public decimal TotalLockedInUsdt { get; set; }
    }
}
