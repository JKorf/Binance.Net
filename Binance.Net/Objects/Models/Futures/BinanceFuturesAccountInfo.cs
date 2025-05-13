namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Information about an account
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesAccountInfo
    {
        /// <summary>
        /// Information about an account assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceFuturesAccountAsset[] Assets { get; set; } = Array.Empty<BinanceFuturesAccountAsset>();

        /// <summary>
        /// Boolean indicating if this account can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }

        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }

        /// <summary>
        /// Boolean indicating if this account can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }

        /// <summary>
        /// Boolean indicating if this account is in multi asset mode
        /// </summary>
        [JsonPropertyName("multiAssetsMargin")]
        public bool MultiAssetsMargin { get; set; }

        /// <summary>
        /// Trade group id
        /// </summary>
        [JsonPropertyName("tradeGroupId")]
        public int TradeGroupId { get; set; }

        /// <summary>
        /// Fee tier
        /// </summary>
        [JsonPropertyName("feeTier")]
        public int FeeTier { get; set; }

        /// <summary>
        /// Maximum withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }

        /// <summary>
        /// Information about an account positions
        /// </summary>
        [JsonPropertyName("positions")]
        public BinancePositionInfoUsdt[] Positions { get; set; } = Array.Empty<BinancePositionInfoUsdt>();

        /// <summary>
        /// Total initial margin
        /// </summary>
        [JsonPropertyName("totalInitialMargin")]
        public decimal TotalInitialMargin { get; set; }

        /// <summary>
        /// Total maintenance margin
        /// </summary>
        [JsonPropertyName("totalMaintMargin")]
        public decimal TotalMaintMargin { get; set; }

        /// <summary>
        /// Total margin balance
        /// </summary>
        [JsonPropertyName("totalMarginBalance")]
        public decimal TotalMarginBalance { get; set; }

        /// <summary>
        /// Total open order initial margin
        /// </summary>
        [JsonPropertyName("totalOpenOrderInitialMargin")]
        public decimal TotalOpenOrderInitialMargin { get; set; }

        /// <summary>
        /// Total positional initial margin
        /// </summary>
        [JsonPropertyName("totalPositionInitialMargin")]
        public decimal TotalPositionInitialMargin { get; set; }

        /// <summary>
        /// Total unrealized profit
        /// </summary>
        [JsonPropertyName("totalUnrealizedProfit")]
        public decimal TotalUnrealizedProfit { get; set; }

        /// <summary>
        /// Total wallet balance
        /// </summary>
        [JsonPropertyName("totalWalletBalance")]
        public decimal TotalWalletBalance { get; set; }

        /// <summary>
        /// Total crossed wallet balance
        /// </summary>
        [JsonPropertyName("totalCrossWalletBalance")]
        public decimal TotalCrossWalletBalance { get; set; }

        /// <summary>
        /// Unrealized profit of crossed positions
        /// </summary>
        [JsonPropertyName("totalCrossUnPnl")]
        public decimal TotalCrossUnPnl { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// The time of account info was updated
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
    }
}
