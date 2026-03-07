namespace Binance.Net.Objects.Models.Spot.GiftCard
{
    /// <summary>
    /// Query results 
    /// </summary>
    public record BinanceGiftCardResponse<T>
    {
        /// <summary>
        /// ["<c>code</c>"] Response code
        /// </summary>
        [JsonPropertyName("code")] 
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>message</c>"] Response message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>data</c>"] Response data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
        /// <summary>
        /// ["<c>success</c>"] Whether the call was successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}