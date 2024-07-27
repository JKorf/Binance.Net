namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn flexible product subscription preview
    /// </summary>
    public record BinanceSimpleEarnFlexiblePreview
    {
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Airdrop asset
        /// </summary>
        [JsonPropertyName("airDropAsset")]
        public string AirDropAsset { get; set; } = string.Empty;
        /// <summary>
        /// Total amount
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// Estimated daily bonus rewards
        /// </summary>
        [JsonPropertyName("estDailyBonusRewards")]
        public decimal EstimatedDailyBonusRewards { get; set; }
        /// <summary>
        /// Estimated daily realtime rewards
        /// </summary>
        [JsonPropertyName("estDailyRealTimeRewards")]
        public decimal EstimatedDailyRealTimeRewards { get; set; }
        /// <summary>
        /// Estimated daily airdrop rewards
        /// </summary>
        [JsonPropertyName("estDailyAirdropRewards")]
        public decimal EstimatedDailyAirdropRewards { get; set; }
    }
}
