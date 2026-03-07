namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Api Key Create Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageApiKeyCreateResult
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>apiKey</c>"] Api Key
        /// </summary>
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>secretKey</c>"] Api Secret
        /// </summary>
        [JsonPropertyName("secretKey")]
        public string ApiSecret { get; set; } = string.Empty;

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