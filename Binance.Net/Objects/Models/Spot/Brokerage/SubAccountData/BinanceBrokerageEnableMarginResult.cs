namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Margin Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageEnableMarginResult
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>enableMargin</c>"] Is Margin Enabled
        /// </summary>
        [JsonPropertyName("enableMargin")]
        public bool IsMarginEnabled { get; set; }

        /// <summary>
        /// ["<c>updateTime</c>"] Update Date
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateDate { get; set; }
    }
}