using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Miner list
    /// </summary>
    [SerializationModel]
    public record BinanceMinerList
    {
        /// <summary>
        /// ["<c>totalNum</c>"] Total number of entries
        /// </summary>
        [JsonPropertyName("totalNum")]
        public int TotalNum { get; set; }
        /// <summary>
        /// ["<c>pageSize</c>"] Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// ["<c>workerDatas</c>"] Worker data
        /// </summary>
        [JsonPropertyName("workerDatas")]
        public BinanceMinerInfo[] WorkerDatas { get; set; } = Array.Empty<BinanceMinerInfo>();
    }

    /// <summary>
    /// Miner details
    /// </summary>
    public record BinanceMinerInfo
    {
        /// <summary>
        /// ["<c>workerId</c>"] Worker id
        /// </summary>
        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>workerName</c>"] Worker name
        /// </summary>
        [JsonPropertyName("workerName")]
        public string WorkerName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public MinerStatus Status { get; set; }
        /// <summary>
        /// ["<c>hashRate</c>"] Hash rate
        /// </summary>
        [JsonPropertyName("hashRate")]
        public decimal HashRate { get; set; }
        /// <summary>
        /// ["<c>dayHashRate</c>"] Day hash rate
        /// </summary>
        [JsonPropertyName("dayHashRate")]
        public decimal DayHashRate { get; set; }
        /// <summary>
        /// ["<c>rejectRate</c>"] Reject rate
        /// </summary>
        [JsonPropertyName("rejectRate")]
        public decimal RejectRate { get; set; }
        /// <summary>
        /// ["<c>lastShareTime</c>"] Last share time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("lastShareTime")]
        public DateTime LastShareTime { get; set; }
    }
}

