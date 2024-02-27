using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn flexible product reward record
    /// </summary>
    public class BinanceSimpleEarnFlexibleRewardRecord
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Project id
        /// </summary>
        [JsonProperty("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Rewards
        /// </summary>
        [JsonProperty("rewards")]
        public decimal Rewards { get; set; }
        /// <summary>
        /// Reward type
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(EnumConverter))]
        public RewardType Type { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
