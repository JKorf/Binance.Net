namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Futures Asset Info
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageFuturesAssetInfo
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public BinanceBrokerageSubAccountFuturesAssetInfo[] Data { get; set; } = Array.Empty<BinanceBrokerageSubAccountFuturesAssetInfo>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Account Futures Asset Info
    /// </summary>
    public record BinanceBrokerageSubAccountFuturesAssetInfo
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Futures enable
        /// </summary>
        [JsonPropertyName("futuresEnable")]
        public bool IsFuturesEnable { get; set; }

        /// <summary>
        /// Total Initial Margin Of Usdt
        /// </summary>
        [JsonPropertyName("totalInitialMarginOfUsdt")]
        public decimal TotalInitialMarginOfUsdt { get; set; }

        /// <summary>
        /// Total Maintenance Margin Of Usdt
        /// </summary>
        [JsonPropertyName("totalMaintenanceMarginOfUsdt")]
        public decimal TotalMaintenanceMarginOfUsdt { get; set; }

        /// <summary>
        /// Total Wallet Balance Of Usdt
        /// </summary>
        [JsonPropertyName("totalWalletBalanceOfUsdt")]
        public decimal TotalWalletBalanceOfUsdt { get; set; }

        /// <summary>
        /// Total Unrealized Profit Of Usdt
        /// </summary>
        [JsonPropertyName("totalUnrealizedProfitOfUsdt")]
        public decimal TotalUnrealizedProfitOfUsdt { get; set; }

        /// <summary>
        /// Total Margin Balance Of Usdt
        /// </summary>
        [JsonPropertyName("totalMarginBalanceOfUsdt")]
        public decimal TotalMarginBalanceOfUsdt { get; set; }

        /// <summary>
        /// Total Position Initial Margin Of Usdt
        /// </summary>
        [JsonPropertyName("totalPositionInitialMarginOfUsdt")]
        public decimal TotalPositionInitialMarginOfUsdt { get; set; }

        /// <summary>
        /// Total Open Order Initial Margin Of Usdt
        /// </summary>
        [JsonPropertyName("totalOpenOrderInitialMarginOfUsdt")]
        public decimal TotalOpenOrderInitialMarginOfUsdt { get; set; }
    }
}