namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Information about an account
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesAccountInfo
    {
        /// <summary>
        /// ["<c>assets</c>"] Information about an account assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceFuturesAccountAsset[] Assets { get; set; } = Array.Empty<BinanceFuturesAccountAsset>();

        /// <summary>
        /// ["<c>canDeposit</c>"] Boolean indicating if this account can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }

        /// <summary>
        /// ["<c>canTrade</c>"] Boolean indicating if this account can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }

        /// <summary>
        /// ["<c>canWithdraw</c>"] Boolean indicating if this account can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }

        /// <summary>
        /// ["<c>feeBurn</c>"] Boolean indicating if this account has fee burn on or off
        /// </summary>
        [JsonPropertyName("feeBurn")]
        public bool FeeBurn { get; set; }

        /// <summary>
        /// ["<c>multiAssetsMargin</c>"] Boolean indicating if this account is in multi asset mode
        /// </summary>
        [JsonPropertyName("multiAssetsMargin")]
        public bool MultiAssetsMargin { get; set; }

        /// <summary>
        /// ["<c>tradeGroupId</c>"] Trade group id
        /// </summary>
        [JsonPropertyName("tradeGroupId")]
        public int TradeGroupId { get; set; }

        /// <summary>
        /// ["<c>feeTier</c>"] Fee tier
        /// </summary>
        [JsonPropertyName("feeTier")]
        public int FeeTier { get; set; }

        /// <summary>
        /// ["<c>maxWithdrawAmount</c>"] Maximum withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }

        /// <summary>
        /// ["<c>positions</c>"] Information about an account positions
        /// </summary>
        [JsonPropertyName("positions")]
        public BinancePositionInfoUsdt[] Positions { get; set; } = Array.Empty<BinancePositionInfoUsdt>();

        /// <summary>
        /// ["<c>totalInitialMargin</c>"] Total initial margin
        /// </summary>
        [JsonPropertyName("totalInitialMargin")]
        public decimal TotalInitialMargin { get; set; }

        /// <summary>
        /// ["<c>totalMaintMargin</c>"] Total maintenance margin
        /// </summary>
        [JsonPropertyName("totalMaintMargin")]
        public decimal TotalMaintMargin { get; set; }

        /// <summary>
        /// ["<c>totalMarginBalance</c>"] Total margin balance
        /// </summary>
        [JsonPropertyName("totalMarginBalance")]
        public decimal TotalMarginBalance { get; set; }

        /// <summary>
        /// ["<c>totalOpenOrderInitialMargin</c>"] Total open order initial margin
        /// </summary>
        [JsonPropertyName("totalOpenOrderInitialMargin")]
        public decimal TotalOpenOrderInitialMargin { get; set; }

        /// <summary>
        /// ["<c>totalPositionInitialMargin</c>"] Total positional initial margin
        /// </summary>
        [JsonPropertyName("totalPositionInitialMargin")]
        public decimal TotalPositionInitialMargin { get; set; }

        /// <summary>
        /// ["<c>totalUnrealizedProfit</c>"] Total unrealized profit
        /// </summary>
        [JsonPropertyName("totalUnrealizedProfit")]
        public decimal TotalUnrealizedProfit { get; set; }

        /// <summary>
        /// ["<c>totalWalletBalance</c>"] Total wallet balance
        /// </summary>
        [JsonPropertyName("totalWalletBalance")]
        public decimal TotalWalletBalance { get; set; }

        /// <summary>
        /// ["<c>totalCrossWalletBalance</c>"] Total crossed wallet balance
        /// </summary>
        [JsonPropertyName("totalCrossWalletBalance")]
        public decimal TotalCrossWalletBalance { get; set; }

        /// <summary>
        /// ["<c>totalCrossUnPnl</c>"] Unrealized profit of crossed positions
        /// </summary>
        [JsonPropertyName("totalCrossUnPnl")]
        public decimal TotalCrossUnPnl { get; set; }

        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// ["<c>updateTime</c>"] The time of account info was updated
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
    }
}

