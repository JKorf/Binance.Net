namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Create Result
    /// </summary>
    public record BinanceBrokerageSubAccountCreateResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subAccountId")]
        public string SubAccountId { get; set; } = string.Empty;
        /// <summary>
        /// Email
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Tag
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;
    }
}