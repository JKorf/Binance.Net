namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account details
    /// </summary>
    public record BinanceSubAccountBlvt
    {
        /// <summary>
        /// The email associated with the sub account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;      
        /// <summary>
        /// Blvt enabled
        /// </summary>
        [JsonPropertyName("enableBlvt")]
        public bool EnableBlvt { get; set; }
    }
}
