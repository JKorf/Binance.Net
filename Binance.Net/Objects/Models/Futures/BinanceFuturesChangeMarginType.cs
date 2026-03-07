namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Result from a change margin type request
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesChangeMarginTypeResult
    {
        /// <summary>
        /// ["<c>code</c>"] Response code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// ["<c>msg</c>"] Response message
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
    }
}

