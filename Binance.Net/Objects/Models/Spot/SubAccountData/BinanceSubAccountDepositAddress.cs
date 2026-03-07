namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Deposit address info for a sub-account
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountDepositAddress
    {
        /// <summary>
        /// ["<c>address</c>"] The deposit address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coin</c>"] Asset type
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tag</c>"] Tag for the deposit address
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>url</c>"] Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}

