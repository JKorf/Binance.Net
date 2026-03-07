namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account details
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountBlvt
    {
        /// <summary>
        /// ["<c>email</c>"] The email associated with the sub account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>enableBlvt</c>"] Blvt enabled
        /// </summary>
        [JsonPropertyName("enableBlvt")]
        public bool EnableBlvt { get; set; }
    }
}

