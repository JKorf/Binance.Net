namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Leverage Token Result
    /// </summary>
    public record BinanceBrokerageEnableLeverageTokenResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Is Leverage Token Enabled
        /// </summary>
        [JsonPropertyName("enableBlvt")]
        public bool IsLeverageTokenEnabled { get; set; }
    }
}