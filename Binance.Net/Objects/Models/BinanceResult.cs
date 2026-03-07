namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Query result
    /// </summary>
    [SerializationModel]
    public record BinanceResult
    {
        /// <summary>
        /// ["<c>code</c>"] Result code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// ["<c>msg</c>"] Message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Query result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[SerializationModel]
    internal record BinanceResult<T> : BinanceResult
    {
        /// <summary>
        /// ["<c>data</c>"] The data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}

