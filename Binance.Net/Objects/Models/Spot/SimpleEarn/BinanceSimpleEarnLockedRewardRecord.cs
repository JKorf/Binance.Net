namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn locked product reward record
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnLockedRewardRecord
    {
        /// <summary>
        /// Reward asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public long PositionId { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Lock period
        /// </summary>
        [JsonPropertyName("lockPeriod")]
        public int LockPeriod { get; set; }
        /// <summary>
        /// Reward timestamp.
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Rewards type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
