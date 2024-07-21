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
        public string WorkerName { get; set; } = string.Empty;

        /// <summary>
        /// Data type
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Hash rate data
        /// </summary>
        public IEnumerable<BinanceHashRate> HashRateDatas { get; set; } = Array.Empty<BinanceHashRate>();
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
        public decimal HashRate { get; set; }
        /// <summary>
        /// Rejected
        /// </summary>
        public decimal Reject { get; set; }
    }
}
