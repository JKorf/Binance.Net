namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Api Key
    /// </summary>
    public record BinanceBrokerageSubAccountApiKey
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Api Key
        /// </summary>
        [JsonPropertyName("apikey")]
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Is Spot Trading Enabled
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool IsSpotTradingEnabled { get; set; }

        /// <summary>
        /// Is Margin Trading Enabled
        /// </summary>
        [JsonPropertyName("marginTrade")]
        public bool IsMarginTradingEnabled { get; set; }

        /// <summary>
        /// Is Futures Trading Enabled
        /// </summary>
        [JsonPropertyName("futuresTrade")]
        public bool IsFuturesTradingEnabled { get; set; }
    }
}