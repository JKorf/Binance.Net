namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Information about an account asset
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesAccountAsset
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>initialMargin</c>"] Initial Margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// ["<c>maintMargin</c>"] Maintenance Margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintMargin { get; set; }

        /// <summary>
        /// ["<c>marginBalance</c>"] Margin Balance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }

        /// <summary>
        /// ["<c>maxWithdrawAmount</c>"] Maximum Withdraw Quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }

        /// <summary>
        /// ["<c>openOrderInitialMargin</c>"] Open Order Initial Margin
        /// </summary>
        [JsonPropertyName("openOrderInitialMargin")]
        public decimal OpenOrderInitialMargin { get; set; }

        /// <summary>
        /// ["<c>positionInitialMargin</c>"] Position Initial Margin
        /// </summary>
        [JsonPropertyName("positionInitialMargin")]
        public decimal PositionInitialMargin { get; set; }

        /// <summary>
        /// ["<c>unrealizedProfit</c>"] Unrealized Profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// ["<c>walletBalance</c>"] Wallet Balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }

        /// <summary>
        /// ["<c>crossWalletBalance</c>"] Crossed Wallet Balance
        /// </summary>
        [JsonPropertyName("crossWalletBalance")]
        public decimal CrossWalletBalance { get; set; }

        /// <summary>
        /// ["<c>crossUnPnl</c>"] Unrealized profit of crossed positions
        /// </summary>
        [JsonPropertyName("crossUnPnl")]
        public decimal CrossUnrealizedPnl { get; set; }

        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// ["<c>marginAvailable</c>"] Whether the asset can be used as margin in Multi-Assets mode
        /// </summary>
        [JsonPropertyName("marginAvailable")]
        public bool? MarginAvailable { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }
}

