namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit address info
    /// </summary>
    [SerializationModel]
    public record BinanceDepositAddress
    {
        /// <summary>
        /// ["<c>address</c>"] The deposit address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>url</c>"] The address URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        /// <summary>
        /// ["<c>tag</c>"] Address tag
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coin</c>"] Asset the address is for
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isDefault</c>"] Whether this is the default address.
        /// </summary>
        [JsonPropertyName("isDefault")]
        public bool? IsDefault { get; set; }
    }
}

