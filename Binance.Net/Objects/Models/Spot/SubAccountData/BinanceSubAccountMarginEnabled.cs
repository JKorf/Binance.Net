namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account margin trading enabled
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountMarginEnabled
    {
        /// <summary>
        /// Email of the account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Whether Margin trading is enabled
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }
    }
}
