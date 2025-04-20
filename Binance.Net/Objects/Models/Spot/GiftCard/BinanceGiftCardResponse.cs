namespace Binance.Net.Objects.Models.Spot.GiftCard
{
    /// <summary>
    /// Query results 
    /// </summary>
    public record BinanceGiftCardResponse<T>
    {
        /// <summary>
        /// Response code
        /// </summary>
        [JsonPropertyName("code")] 
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Response message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Response data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; }
        /// <summary>
        /// Whether the call was successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}