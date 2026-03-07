namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Spot Asset Info
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageSpotAssetInfo
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public BinanceBrokerageSubAccountSpotAssetInfo[] Data { get; set; } = Array.Empty<BinanceBrokerageSubAccountSpotAssetInfo>();

        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
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
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>totalBalanceOfBtc</c>"] Total Balance Of Btc
        /// </summary>
        [JsonPropertyName("totalBalanceOfBtc")]
        public decimal TotalBalanceOfBtc { get; set; }
    }
}