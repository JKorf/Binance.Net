namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Miner details
    /// </summary>
    public record BinanceMinerDetails
    {
        /// <summary>
        /// Name of the worker
        /// </summary>
        [JsonPropertyName("workerName")]
        public string WorkerName { get; set; } = string.Empty;

        /// <summary>
        /// Data type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Hash rate data
        /// </summary>
        [JsonPropertyName("hashRateDatas")]
        public BinanceHashRate[] HashRateDatas { get; set; } = Array.Empty<BinanceHashRate>();
    }

    /// <summary>
    /// Hash rate
    /// </summary>
    public record BinanceHashRate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Hashrate
        /// </summary>
        [JsonPropertyName("hashRate")]
        public decimal HashRate { get; set; }
        /// <summary>
        /// Rejected
        /// </summary>
        [JsonPropertyName("reject")]
        public decimal Reject { get; set; }
    }
}
