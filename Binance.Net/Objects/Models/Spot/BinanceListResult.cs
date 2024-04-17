namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// List result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinanceListResult<T>
    {
        /// <summary>
        /// Data start time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Emd to,e
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Limit
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// More data available
        /// </summary>
        public bool MoreData { get; set; }
        /// <summary>
        /// The data
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<T> Data { get; set; } = Array.Empty<T>();
    }
}
