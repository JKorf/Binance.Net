using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Mining
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
        public decimal ProfitToday { get; set; }
        /// <summary>
        /// Yesterdays profit
        /// </summary>
        public decimal ProfitYesterday { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public decimal UserName { get; set; }
        /// <summary>
        /// Hashrate unit
        /// </summary>
        public decimal Unit { get; set; }
        /// <summary>
        /// Algorithm
        /// </summary>
        [JsonProperty("algo")]
        public decimal Algorithm { get; set; }
    }
}
