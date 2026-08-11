namespace Binance.Net.Objects.Models.General.Affiliate
{
    [SerializationModel]
    public record BinanceAffiliateResponse<T>
    {
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("code")]
        public string code { get; set; }
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
