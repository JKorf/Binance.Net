namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Transaction download info
    /// </summary>
    public record BinanceFuturesDownloadIdInfo
    {
        /// <summary>
        /// Average time taken for data download in the past 30 days
        /// </summary>
        [JsonProperty("avgCostTimestampOfLast30d")]
        public long AverageCostTimestampOfLast30Days { get; set; }
        /// <summary>
        /// Download id
        /// </summary>
        public string DownloadId { get; set; } = string.Empty;
    }
}
