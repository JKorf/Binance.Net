namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Query results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record BinanceListRecords<T>
    {
        /// <summary>
        /// The total count of the records
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// The list records
        /// </summary>
        [JsonPropertyName("list")]
        public IEnumerable<T> List { get; set; } = Array.Empty<T>();
    }
}
