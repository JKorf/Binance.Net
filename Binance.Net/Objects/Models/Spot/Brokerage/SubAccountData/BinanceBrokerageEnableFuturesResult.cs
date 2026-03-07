namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Futures Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageEnableFuturesResult
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>enableFutures</c>"] Is Futures Enabled
        /// </summary>
        [JsonPropertyName("enableFutures")]
        public bool IsFuturesEnabled { get; set; }

        /// <summary>
        /// ["<c>updateTime</c>"] Update Date
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateDate { get; set; }
    }
}