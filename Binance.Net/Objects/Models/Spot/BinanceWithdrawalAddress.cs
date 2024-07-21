namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdrawal address info
    /// </summary>
    public record BinanceWithdrawalAddress
    {
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Address tag
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string? AddressTag { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Origin
        /// </summary>
        [JsonPropertyName("origin")]
        public string? Origin { get; set; }
        /// <summary>
        /// Origin type
        /// </summary>
        [JsonPropertyName("originType")]
        public string OriginType { get; set; } = string.Empty;
        /// <summary>
        /// Is whitelisted
        /// </summary>
        [JsonPropertyName("whiteStatus")]
        public bool Whitelisted { get; set; }
    }
}
