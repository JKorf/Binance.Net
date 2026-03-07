namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Margin Asset Info
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageMarginAssetInfo
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public BinanceBrokerageSubAccountMarginAssetInfo[] Data { get; set; } = Array.Empty<BinanceBrokerageSubAccountMarginAssetInfo>();

        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
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
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>marginEnable</c>"] Margin enable
        /// </summary>
        [JsonPropertyName("marginEnable")]
        public bool IsMarginEnable { get; set; }

        /// <summary>
        /// ["<c>totalAssetOfBtc</c>"] Total Asset Of Btc
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }

        /// <summary>
        /// ["<c>totalLiabilityBtc</c>"] Total Liability Of Btc
        /// </summary>
        [JsonPropertyName("totalLiabilityBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }

        /// <summary>
        /// ["<c>totalNetAssetOfBtc</c>"] Total Net Asset Of Btc
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }

        /// <summary>
        /// ["<c>marginLevel</c>"] Margin level
        /// </summary>
        [JsonPropertyName("marginLevel")]
        public decimal MarginLevel { get; set; }
    }
}