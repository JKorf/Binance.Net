namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining account
    /// </summary>
    [SerializationModel]
    public record BinanceMiningAccount
    {
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>userName</c>"] User name
        /// </summary>
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>list</c>"] Hash rates
        /// </summary>
        [JsonPropertyName("list")]
        public BinanceHashRate[] Hashrates { get; set; } = Array.Empty<BinanceHashRate>();
    }
}

