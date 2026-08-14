namespace Binance.Net.Objects.Models.General.Affiliate
{
    /// <summary>
    /// Binance Affiliate Performance data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SerializationModel]
    public record BinanceAffiliateResponse<T>
    {
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("code")]
        public string code { get; set; } = string.Empty;
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
