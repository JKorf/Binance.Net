namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Futures Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageEnableFuturesResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Is Futures Enabled
        /// </summary>
        [JsonPropertyName("enableFutures")]
        public bool IsFuturesEnabled { get; set; }

        /// <summary>
        /// Update Date
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateDate { get; set; }
    }
}