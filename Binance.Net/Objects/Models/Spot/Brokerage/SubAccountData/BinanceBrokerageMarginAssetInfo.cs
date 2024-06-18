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
        public IEnumerable<BinanceBrokerageSubAccountMarginAssetInfo> Data { get; set; } = Array.Empty<BinanceBrokerageSubAccountMarginAssetInfo>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
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
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Margin enable
        /// </summary>
        [JsonProperty("marginEnable")]
        public bool IsMarginEnable { get; set; }
        
        /// <summary>
        /// Total Asset Of Btc
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        
        /// <summary>
        /// Total Liability Of Btc
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        
        /// <summary>
        /// Total Net Asset Of Btc
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
        
        /// <summary>
        /// Margin level
        /// </summary>
        public decimal MarginLevel { get; set; }
    }
}