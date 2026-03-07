namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Leverage Token Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageEnableLeverageTokenResult
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>enableBlvt</c>"] Is Leverage Token Enabled
        /// </summary>
        [JsonPropertyName("enableBlvt")]
        public bool IsLeverageTokenEnabled { get; set; }
    }
}