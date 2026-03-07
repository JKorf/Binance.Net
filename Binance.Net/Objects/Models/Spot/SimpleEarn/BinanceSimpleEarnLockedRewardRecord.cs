namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn locked product reward record
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnLockedRewardRecord
    {
        /// <summary>
        /// ["<c>asset</c>"] Reward asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionId</c>"] Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public long PositionId { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>lockPeriod</c>"] Lock period
        /// </summary>
        [JsonPropertyName("lockPeriod")]
        public int LockPeriod { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Reward timestamp.
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Rewards type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}

