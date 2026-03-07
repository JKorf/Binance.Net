namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Query results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record BinanceListRecords<T>
    {
        /// <summary>
        /// ["<c>total</c>"] The total count of the records
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>list</c>"] The list records
        /// </summary>
        [JsonPropertyName("list")]
        public IEnumerable<T> List { get; set; } = Array.Empty<T>();
    }
}

