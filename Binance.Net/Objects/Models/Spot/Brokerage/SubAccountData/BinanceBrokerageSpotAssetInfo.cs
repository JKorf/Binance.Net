namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Spot Asset Info
    /// </summary>
    public record BinanceBrokerageSpotAssetInfo
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<BinanceBrokerageSubAccountSpotAssetInfo> Data { get; set; } = Array.Empty<BinanceBrokerageSubAccountSpotAssetInfo>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Account Spot Asset Info
    /// </summary>
    public record BinanceBrokerageSubAccountSpotAssetInfo
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subAccountId")]
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Total Balance Of Btc
        /// </summary>
        [JsonPropertyName("totalBalanceOfBtc")]
        public decimal TotalBalanceOfBtc { get; set; }
    }
}