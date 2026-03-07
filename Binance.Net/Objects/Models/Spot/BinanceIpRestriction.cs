namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// IP restriction info
    /// </summary>
    [SerializationModel]
    public record BinanceIpRestriction
    {
        /// <summary>
        /// ["<c>ipRestrict</c>"] Is currently restricted
        /// </summary>
        [JsonPropertyName("ipRestrict")]
        public bool IpRestricted { get; set; }
        /// <summary>
        /// ["<c>ipList</c>"] IP whitelist.
        /// </summary>
        [JsonPropertyName("ipList")]
        public string[] IpList { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>updateTime</c>"] Update Time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>apiKey</c>"] The API key
        /// </summary>
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; } = string.Empty;
    }
}

