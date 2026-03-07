namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Miner details
    /// </summary>
    [SerializationModel]
    public record BinanceMinerDetails
    {
        /// <summary>
        /// ["<c>workerName</c>"] Name of the worker
        /// </summary>
        [JsonPropertyName("workerName")]
        public string WorkerName { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>type</c>"] Data type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>hashrateDatas</c>"] Hash rate data
        /// </summary>
        [JsonPropertyName("hashrateDatas")]
        public BinanceHashRate[] HashRateDatas { get; set; } = Array.Empty<BinanceHashRate>();
    }

    /// <summary>
    /// Hash rate
    /// </summary>
    public record BinanceHashRate
    {
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>hashrate</c>"] Hashrate
        /// </summary>
        [JsonPropertyName("hashrate")]
        public decimal HashRate { get; set; }
        /// <summary>
        /// ["<c>reject</c>"] Rejected
        /// </summary>
        [JsonPropertyName("reject")]
        public decimal Reject { get; set; }
    }
}

