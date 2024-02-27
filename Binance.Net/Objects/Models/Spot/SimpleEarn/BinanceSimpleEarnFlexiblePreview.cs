using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn flexible product subscription preview
    /// </summary>
    public class BinanceSimpleEarnFlexiblePreview
    {
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonProperty("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Airdrop asset
        /// </summary>
        [JsonProperty("airDropAsset")]
        public string AirDropAsset { get; set; } = string.Empty;
        /// <summary>
        /// Total amount
        /// </summary>
        [JsonProperty("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// Estimated daily bonus rewards
        /// </summary>
        [JsonProperty("estDailyBonusRewards")]
        public decimal EstimatedDailyBonusRewards { get; set; }
        /// <summary>
        /// Estimated daily realtime rewards
        /// </summary>
        [JsonProperty("estDailyRealTimeRewards")]
        public decimal EstimatedDailyRealTimeRewards { get; set; }
        /// <summary>
        /// Estimated daily airdrop rewards
        /// </summary>
        [JsonProperty("estDailyAirdropRewards")]
        public decimal EstimatedDailyAirdropRewards { get; set; }
    }
}
