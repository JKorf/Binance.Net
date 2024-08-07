namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Information about an account asset
    /// </summary>
    public record BinanceFuturesAccountAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Initial Margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// Maint Margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintMargin { get; set; }

        /// <summary>
        /// Margin Balance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }

        /// <summary>
        /// Maximum Withdraw Quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }

        /// <summary>
        /// Open Order Initial Margin
        /// </summary>
        [JsonPropertyName("openOrderInitialMargin")]
        public decimal OpenOrderInitialMargin { get; set; }

        /// <summary>
        /// Position Initial Margin
        /// </summary>
        [JsonPropertyName("positionInitialMargin")]
        public decimal PositionInitialMargin { get; set; }

        /// <summary>
        /// Unrealized Profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// Wallet Balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }

        /// <summary>
        /// Crossed Wallet Balance
        /// </summary>
        [JsonPropertyName("crossWalletBalance")]
        public decimal CrossWalletBalance { get; set; }

        /// <summary>
        /// Unrealized profit of crossed positions
        /// </summary>
        [JsonPropertyName("crossUnPnl")]
        public decimal CrossUnrealizedPnl { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Whether the asset can be used as margin in Multi-Assets mode
        /// </summary>
        [JsonPropertyName("marginAvailable")]
        public bool? MarginAvailable { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }
}
