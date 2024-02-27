using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Purchase id
    /// </summary>
    public class BinanceSimpleEarnPurchase
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Purchase id
        /// </summary>
        [JsonProperty("purchaseId")]
        public long PurchaseId { get; set; }
        /// <summary>
        /// Position id
        /// </summary>
        [JsonProperty("positionId")]
        public string? PositionId { get; set; }
    }
}
