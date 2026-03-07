namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Query results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[SerializationModel]
    public record BinanceQueryRecords<T>
    {
        /// <summary>
        /// ["<c>rows</c>"] The list records
        /// </summary>
        [JsonPropertyName("rows")]
        public T[] Rows { get; set; } = Array.Empty<T>();
        /// <summary>
        /// ["<c>total</c>"] The total count of the records
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}

