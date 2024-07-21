namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Interest rate history
    /// </summary>
    public record BinanceInterestRateHistory
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The daily interest
        /// </summary>
        [JsonPropertyName("dailyInterestRate")]
        public decimal DailyInterest { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Vip level
        /// </summary>
        public string VipLevel { get; set; } = string.Empty;
    }
}
