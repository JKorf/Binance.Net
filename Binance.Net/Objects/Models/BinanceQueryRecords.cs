namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Query results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record BinanceQueryRecords<T>
    {
        /// <summary>
        /// The list records
        /// </summary>
        public IEnumerable<T> Rows { get; set; } = Array.Empty<T>();
        /// <summary>
        /// The total count of the records
        /// </summary>
        public int Total { get; set; }
    }
}
