namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account futures trading enabled
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountFuturesEnabled
    {
        /// <summary>
        /// ["<c>email</c>"] Email of the account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isFuturesEnabled</c>"] Whether futures trading is enabled
        /// </summary>
        [JsonPropertyName("isFuturesEnabled")]
        public bool IsFuturesEnabled { get; set; }
    }
}

