using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Redemption
    /// </summary>
    public class BinanceSimpleEarnRedemption
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Redeem id
        /// </summary>
        [JsonProperty("redeemId")]
        public long RedeemId { get; set; }
    }
}
