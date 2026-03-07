namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Create Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageSubAccountCreateResult
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>email</c>"] Email
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tag</c>"] Tag
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;
    }
}