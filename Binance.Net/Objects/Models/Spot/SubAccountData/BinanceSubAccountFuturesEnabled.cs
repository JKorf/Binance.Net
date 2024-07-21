namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account futures trading enabled
    /// </summary>
    public record BinanceSubAccountFuturesEnabled
    {
        /// <summary>
        /// Email of the account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Whether futures trading is enabled
        /// </summary>
        [JsonPropertyName("isFuturesEnabled")]
        public bool IsFuturesEnabled { get; set; }
    }
}
