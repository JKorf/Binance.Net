using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Redemption quota left
    /// </summary>
    public class BinanceRedemptionQuotaLeft
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Daily quota
        /// </summary>
        public decimal DailyQuota { get; set; }
        /// <summary>
        /// Left quota
        /// </summary>
        public decimal LeftQuota { get; set; }
        /// <summary>
        /// Minimal redemption amount
        /// </summary>
        [JsonProperty("minRedemptionAmount")]
        public decimal MinimalRedemptionAmount { get; set; }
    }
}
