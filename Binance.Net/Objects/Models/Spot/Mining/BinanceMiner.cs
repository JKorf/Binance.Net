using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Miner list
    /// </summary>
    public record BinanceMinerList
    {
        /// <summary>
        /// Total number of entries
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Worker data
        /// </summary>
        public IEnumerable<BinanceMinerInfo> WorkerDatas { get; set; } = Array.Empty<BinanceMinerInfo>();
    }

    /// <summary>
    /// Miner details
    /// </summary>
    public record BinanceMinerInfo
    {
        /// <summary>
        /// Worker id
        /// </summary>
        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; } = string.Empty;
        /// <summary>
        /// Worker name
        /// </summary>
        [JsonPropertyName("workerName")]
        public string WorkerName { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public MinerStatus Status { get; set; }
        /// <summary>
        /// Hash rate
        /// </summary>
        [JsonPropertyName("hashRate")]
        public decimal HashRate { get; set; }
        /// <summary>
        /// Day hash rate
        /// </summary>
        [JsonPropertyName("dayHashRate")]
        public decimal DayHashRate { get; set; }
        /// <summary>
        /// Reject rate
        /// </summary>
        [JsonPropertyName("rejectRate")]
        public decimal RejectRate { get; set; }
        /// <summary>
        /// Last share time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("lastShareTime")]
        public DateTime LastShareTime { get; set; }
    }
}
