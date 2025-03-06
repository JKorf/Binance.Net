namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Margin Asset Info
    /// </summary>
    public record BinanceBrokerageMarginAssetInfo
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public BinanceBrokerageSubAccountMarginAssetInfo[] Data { get; set; } = Array.Empty<BinanceBrokerageSubAccountMarginAssetInfo>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Account Margin Asset Info
    /// </summary>
    public record BinanceBrokerageSubAccountMarginAssetInfo
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Margin enable
        /// </summary>
        [JsonPropertyName("marginEnable")]
        public bool IsMarginEnable { get; set; }

        /// <summary>
        /// Total Asset Of Btc
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }

        /// <summary>
        /// Total Liability Of Btc
        /// </summary>
        [JsonPropertyName("totalLiabilityBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }

        /// <summary>
        /// Total Net Asset Of Btc
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }

        /// <summary>
        /// Margin level
        /// </summary>
        [JsonPropertyName("marginLevel")]
        public decimal MarginLevel { get; set; }
    }
}