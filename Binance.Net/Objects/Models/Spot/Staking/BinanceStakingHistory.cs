namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Historical staking info
    /// </summary>
    public record BinanceStakingHistory
    {
        /// <summary>
        /// Position id
        /// </summary>
        public string? PositionId { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Project
        /// </summary>
        public string Project { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Lock period
        /// </summary>
        public int? LockPeriod { get; set; }
        /// <summary>
        /// Redemption date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliverDate { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
