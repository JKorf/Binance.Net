namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Margin Result
    /// </summary>
    public record BinanceBrokerageEnableMarginResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Is Margin Enabled
        /// </summary>
        [JsonPropertyName("enableMargin")]
        public bool IsMarginEnabled { get; set; }
        
        /// <summary>
        /// Update Date
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateDate { get; set; }
    }
}