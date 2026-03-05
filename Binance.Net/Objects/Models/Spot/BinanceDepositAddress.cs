namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit address info
    /// </summary>
    [SerializationModel]
    public record BinanceDepositAddress
    {
        /// <summary>
        /// The deposit address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// The address URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        /// <summary>
        /// Address tag
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// Asset the address is for
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Whether this is the default address.
        /// </summary>
        [JsonPropertyName("isDefault")]
        public bool? IsDefault { get; set; }
    }
}
