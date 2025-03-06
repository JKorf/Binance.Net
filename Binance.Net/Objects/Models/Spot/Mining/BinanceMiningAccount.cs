namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining account
    /// </summary>
    public record BinanceMiningAccount
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// User name
        /// </summary>
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Hash rates
        /// </summary>
        [JsonPropertyName("list")]
        public BinanceHashRate[] Hashrates { get; set; } = Array.Empty<BinanceHashRate>();
    }
}
