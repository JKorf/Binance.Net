namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// List result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record BinanceListResult<T>
    {
        /// <summary>
        /// Data start time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Emd to,e
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Limit
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
        /// <summary>
        /// More data available
        /// </summary>
        [JsonPropertyName("moreData")]
        public bool MoreData { get; set; }
        /// <summary>
        /// The data
        /// </summary>
        [JsonPropertyName("list")]
        public T[] Data { get; set; } = Array.Empty<T>();
    }
}
