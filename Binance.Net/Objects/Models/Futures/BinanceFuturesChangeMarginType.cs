namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Result from a change margin type request
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesChangeMarginTypeResult
    {
        /// <summary>
        /// Response code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// Response message
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
    }
}
