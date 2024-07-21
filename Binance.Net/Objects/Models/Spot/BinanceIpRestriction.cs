namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// IP restriction info
    /// </summary>
    public record BinanceIpRestriction
    {
        /// <summary>
        /// Is currently restricted
        /// </summary>
        [JsonPropertyName("ipRestrict")]
        public bool IpRestricted { get; set; }
        /// <summary>
        /// Ip whitelist
        /// </summary>
        [JsonPropertyName("ipList")]
        public IEnumerable<string> IpList { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Update Time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The API key
        /// </summary>
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; } = string.Empty;
    }
}
