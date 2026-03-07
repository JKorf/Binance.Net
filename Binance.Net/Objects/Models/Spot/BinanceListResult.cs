namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// List result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[SerializationModel]
    public record BinanceListResult<T>
    {
        /// <summary>
        /// ["<c>startTime</c>"] Data start time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// ["<c>endTime</c>"] Data end time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// ["<c>limit</c>"] Limit
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
        /// <summary>
        /// ["<c>moreData</c>"] More data available
        /// </summary>
        [JsonPropertyName("moreData")]
        public bool MoreData { get; set; }
        /// <summary>
        /// ["<c>list</c>"] The data
        /// </summary>
        [JsonPropertyName("list")]
        public T[] Data { get; set; } = Array.Empty<T>();
    }
}

