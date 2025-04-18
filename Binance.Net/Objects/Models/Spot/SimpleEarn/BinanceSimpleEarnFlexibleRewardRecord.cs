using Binance.Net.Converters;
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
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Project id
        /// </summary>
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Rewards
        /// </summary>
        [JsonPropertyName("rewards")]
        public decimal Rewards { get; set; }
        /// <summary>
        /// Reward type
        /// </summary>
        [JsonPropertyName("type")]
        public RewardType Type { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
