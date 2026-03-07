namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining statistics
    /// </summary>
    [SerializationModel]
    public record BinanceMiningStatistic
    {
        /// <summary>
        /// ["<c>fifteenMinHashRate</c>"] Hashrate last fifteen minutes
        /// </summary>
        [JsonPropertyName("fifteenMinHashRate")]
        public decimal FifteenMinuteHashRate { get; set; }
        /// <summary>
        /// ["<c>dayHashRate</c>"] Day hashrate
        /// </summary>
        [JsonPropertyName("dayHashRate")]
        public decimal DayHashRate { get; set; }
        /// <summary>
        /// ["<c>validNum</c>"] Valid shares
        /// </summary>
        [JsonPropertyName("validNum")]
        public int ValidShares { get; set; }
        /// <summary>
        /// ["<c>invalidNum</c>"] Invalid shares
        /// </summary>
        [JsonPropertyName("invalidNum")]
        public int InvalidShares { get; set; }
        /// <summary>
        /// ["<c>profitToday</c>"] Todays profit
        /// </summary>
        [JsonPropertyName("profitToday")]
        public Dictionary<string, decimal> ProfitToday { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// ["<c>profitYesterday</c>"] Yesterdays profit
        /// </summary>
        [JsonPropertyName("profitYesterday")]
        public Dictionary<string, decimal> ProfitYesterday { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// ["<c>userName</c>"] User name
        /// </summary>
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>unit</c>"] Hashrate unit
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>algo</c>"] Algorithm
        /// </summary>
        [JsonPropertyName("algo")]
        public string Algorithm { get; set; } = string.Empty;
    }
}

