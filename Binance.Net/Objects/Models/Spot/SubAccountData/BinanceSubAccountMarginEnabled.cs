namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account margin trading enabled
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountMarginEnabled
    {
        /// <summary>
        /// The account email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Whether margin trading is enabled.
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }
    }
}
