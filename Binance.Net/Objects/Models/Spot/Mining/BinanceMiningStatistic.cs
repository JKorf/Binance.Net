namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining statistics
    /// </summary>
    public class BinanceMiningStatistic
    {
        /// <summary>
        /// Hashrate last fifteen minutes
        /// </summary>
        [JsonProperty("fifteenMinHashRate")]
        public decimal FifteenMinuteHashRate { get; set; }
        /// <summary>
        /// Day hashrate
        /// </summary>
        public decimal DayHashRate { get; set; }
        /// <summary>
        /// Valid shares
        /// </summary>
        [JsonProperty("validNum")]
        public int ValidShares { get; set; }
        /// <summary>
        /// Invalid shares
        /// </summary>
        [JsonProperty("invalidNum")]
        public int InvalidShares { get; set; }
        /// <summary>
        /// Todays profit
        /// </summary>
        public Dictionary<string, decimal> ProfitToday { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Yesterdays profit
        /// </summary>
        public Dictionary<string, decimal> ProfitYesterday { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Hashrate unit
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// Algorithm
        /// </summary>
        [JsonProperty("algo")]
        public string Algorithm { get; set; } = string.Empty;
    }
}
