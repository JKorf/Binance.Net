namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining statistics
    /// </summary>
    [SerializationModel]
    public record BinanceMiningStatistic
    {
        /// <summary>
        /// Hashrate last fifteen minutes
        /// </summary>
        [JsonPropertyName("fifteenMinHashRate")]
        public decimal FifteenMinuteHashRate { get; set; }
        /// <summary>
        /// Day hashrate
        /// </summary>
        [JsonPropertyName("dayHashRate")]
        public decimal DayHashRate { get; set; }
        /// <summary>
        /// Valid shares
        /// </summary>
        [JsonPropertyName("validNum")]
        public int ValidShares { get; set; }
        /// <summary>
        /// Invalid shares
        /// </summary>
        [JsonPropertyName("invalidNum")]
        public int InvalidShares { get; set; }
        /// <summary>
        /// Todays profit
        /// </summary>
        [JsonPropertyName("profitToday")]
        public Dictionary<string, decimal> ProfitToday { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Yesterdays profit
        /// </summary>
        [JsonPropertyName("profitYesterday")]
        public Dictionary<string, decimal> ProfitYesterday { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// User name
        /// </summary>
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Hashrate unit
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// Algorithm
        /// </summary>
        [JsonPropertyName("algo")]
        public string Algorithm { get; set; } = string.Empty;
    }
}
