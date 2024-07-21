namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Query result
    /// </summary>
    public record BinanceResult
    {
        /// <summary>
        /// Result code
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Query result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal record BinanceResult<T>: BinanceResult
    {
        /// <summary>
        /// The data
        /// </summary>
        public T Data { get; set; } = default!;
    }
}
