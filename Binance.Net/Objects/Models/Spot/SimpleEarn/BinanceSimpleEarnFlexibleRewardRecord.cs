using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn flexible product reward record
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnFlexibleRewardRecord
    {
        /// <summary>
        /// ["<c>asset</c>"] Reward asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>projectId</c>"] Project id
        /// </summary>
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rewards</c>"] Rewards
        /// </summary>
        [JsonPropertyName("rewards")]
        public decimal Rewards { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Reward type
        /// </summary>
        [JsonPropertyName("type")]
        public RewardType Type { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Reward timestamp.
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}

