namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Interest rate history
    /// </summary>
    [SerializationModel]
    public record BinanceInterestRateHistory
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>dailyInterestRate</c>"] The daily interest
        /// </summary>
        [JsonPropertyName("dailyInterestRate")]
        public decimal DailyInterest { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>vipLevel</c>"] Vip level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
    }
}

