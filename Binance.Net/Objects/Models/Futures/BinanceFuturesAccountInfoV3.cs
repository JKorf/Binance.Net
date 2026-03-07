using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesAccountInfoV3
    {
        /// <summary>
        /// ["<c>totalInitialMargin</c>"] Total initial margin
        /// </summary>
        [JsonPropertyName("totalInitialMargin")]
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// ["<c>totalMaintMargin</c>"] Total maintenance margin
        /// </summary>
        [JsonPropertyName("totalMaintMargin")]
        public decimal TotalMaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>totalWalletBalance</c>"] Total wallet balance
        /// </summary>
        [JsonPropertyName("totalWalletBalance")]
        public decimal TotalWalletBalance { get; set; }
        /// <summary>
        /// ["<c>totalUnrealizedProfit</c>"] Total unrealized profit
        /// </summary>
        [JsonPropertyName("totalUnrealizedProfit")]
        public decimal TotalUnrealizedProfit { get; set; }
        /// <summary>
        /// ["<c>totalMarginBalance</c>"] Total margin balance
        /// </summary>
        [JsonPropertyName("totalMarginBalance")]
        public decimal TotalMarginBalance { get; set; }
        /// <summary>
        /// ["<c>totalPositionInitialMargin</c>"] Total position initial margin
        /// </summary>
        [JsonPropertyName("totalPositionInitialMargin")]
        public decimal TotalPositionInitialMargin { get; set; }
        /// <summary>
        /// ["<c>totalOpenOrderInitialMargin</c>"] Total open order initial margin
        /// </summary>
        [JsonPropertyName("totalOpenOrderInitialMargin")]
        public decimal TotalOpenOrderInitialMargin { get; set; }
        /// <summary>
        /// ["<c>totalCrossWalletBalance</c>"] Total cross wallet balance
        /// </summary>
        [JsonPropertyName("totalCrossWalletBalance")]
        public decimal TotalCrossWalletBalance { get; set; }
        /// <summary>
        /// ["<c>totalCrossUnPnl</c>"] Total cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("totalCrossUnPnl")]
        public decimal TotalCrossUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>maxWithdrawAmount</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>assets</c>"] Assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceFuturesAccountInfoAsset[] Assets { get; set; } = Array.Empty<BinanceFuturesAccountInfoAsset>();
        /// <summary>
        /// ["<c>positions</c>"] Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public BinanceFuturesAccountInfoPosition[] Positions { get; set; } = Array.Empty<BinanceFuturesAccountInfoPosition>();
    }

    /// <summary>
    /// Asset information
    /// </summary>
    public record BinanceFuturesAccountInfoAsset
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>walletBalance</c>"] Wallet balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// ["<c>unrealizedProfit</c>"] Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
        /// <summary>
        /// ["<c>marginBalance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>maintMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>positionInitialMargin</c>"] Position initial margin
        /// </summary>
        [JsonPropertyName("positionInitialMargin")]
        public decimal PositionInitialMargin { get; set; }
        /// <summary>
        /// ["<c>openOrderInitialMargin</c>"] Open order initial margin
        /// </summary>
        [JsonPropertyName("openOrderInitialMargin")]
        public decimal OpenOrderInitialMargin { get; set; }
        /// <summary>
        /// ["<c>crossWalletBalance</c>"] Cross wallet balance
        /// </summary>
        [JsonPropertyName("crossWalletBalance")]
        public decimal CrossWalletBalance { get; set; }
        /// <summary>
        /// ["<c>crossUnPnl</c>"] Cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("crossUnPnl")]
        public decimal CrossUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>maxWithdrawAmount</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Position info
    /// </summary>
    public record BinanceFuturesAccountInfoPosition
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>positionAmt</c>"] Position amount
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal PositionAmount { get; set; }
        /// <summary>
        /// ["<c>unrealizedProfit</c>"] Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>notional</c>"] Notional
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal Notional { get; set; }
        /// <summary>
        /// ["<c>isolatedWallet</c>"] Isolated wallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal IsolatedWallet { get; set; }
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>maintMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }


}

