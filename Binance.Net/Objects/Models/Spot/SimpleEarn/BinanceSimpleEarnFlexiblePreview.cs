namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn flexible product subscription preview
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnFlexiblePreview
    {
        /// <summary>
        /// ["<c>rewardAsset</c>"] Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>airDropAsset</c>"] Airdrop asset
        /// </summary>
        [JsonPropertyName("airDropAsset")]
        public string AirDropAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalAmount</c>"] Total amount.
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// ["<c>estDailyBonusRewards</c>"] Estimated daily bonus rewards
        /// </summary>
        [JsonPropertyName("estDailyBonusRewards")]
        public decimal EstimatedDailyBonusRewards { get; set; }
        /// <summary>
        /// ["<c>estDailyRealTimeRewards</c>"] Estimated daily realtime rewards
        /// </summary>
        [JsonPropertyName("estDailyRealTimeRewards")]
        public decimal EstimatedDailyRealTimeRewards { get; set; }
        /// <summary>
        /// ["<c>estDailyAirdropRewards</c>"] Estimated daily airdrop rewards
        /// </summary>
        [JsonPropertyName("estDailyAirdropRewards")]
        public decimal EstimatedDailyAirdropRewards { get; set; }
    }
}

