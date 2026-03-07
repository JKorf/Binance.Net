namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Api Key
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageSubAccountApiKey
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>apikey</c>"] Api Key
        /// </summary>
        [JsonPropertyName("apikey")]
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>canTrade</c>"] Is Spot Trading Enabled
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool IsSpotTradingEnabled { get; set; }

        /// <summary>
        /// ["<c>marginTrade</c>"] Is Margin Trading Enabled
        /// </summary>
        [JsonPropertyName("marginTrade")]
        public bool IsMarginTradingEnabled { get; set; }

        /// <summary>
        /// ["<c>futuresTrade</c>"] Is Futures Trading Enabled
        /// </summary>
        [JsonPropertyName("futuresTrade")]
        public bool IsFuturesTradingEnabled { get; set; }
    }
}