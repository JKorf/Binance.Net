namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdrawal address info
    /// </summary>
    [SerializationModel]
    public record BinanceWithdrawalAddress
    {
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>addressTag</c>"] Address tag
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string? AddressTag { get; set; }
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>origin</c>"] Origin
        /// </summary>
        [JsonPropertyName("origin")]
        public string? Origin { get; set; }
        /// <summary>
        /// ["<c>originType</c>"] Origin type
        /// </summary>
        [JsonPropertyName("originType")]
        public string OriginType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>whiteStatus</c>"] Is whitelisted
        /// </summary>
        [JsonPropertyName("whiteStatus")]
        public bool Whitelisted { get; set; }
    }
}

