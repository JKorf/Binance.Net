namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceFuturesAccountInfo
    {
        /// <summary>
        /// Information about an account assets
        /// </summary>
        public IEnumerable<BinanceFuturesAccountAsset> Assets { get; set; } = Array.Empty<BinanceFuturesAccountAsset>();

        /// <summary>
        /// Boolean indicating if this account can deposit
        /// </summary>
        public bool CanDeposit { get; set; }

        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        public bool CanTrade { get; set; }

        /// <summary>
        /// Boolean indicating if this account can withdraw
        /// </summary>
        public bool CanWithdraw { get; set; }

        /// <summary>
        /// Boolean indicating if this account is in multi asset mode
        /// </summary>
        [JsonProperty("multiAssetsMargin")]
        public bool MultiAssetsMargin { get; set; }

        /// <summary>
        /// Trade group id
        /// </summary>
        [JsonProperty("tradeGroupId")]
        public int TradeGroupId { get; set; }

        /// <summary>
        /// Fee tier
        /// </summary>
        public int FeeTier { get; set; }

        /// <summary>
        /// Maximum withdraw quantity
        /// </summary>
        [JsonProperty("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }

        /// <summary>
        /// Information about an account positions
        /// </summary>
        public IEnumerable<BinancePositionInfoUsdt> Positions { get; set; } = Array.Empty<BinancePositionInfoUsdt>();

        /// <summary>
        /// Total initial margin
        /// </summary>
        public decimal TotalInitialMargin { get; set; }

        /// <summary>
        /// Total maint margin
        /// </summary>
        public decimal TotalMaintMargin { get; set; }

        /// <summary>
        /// Total margin balance
        /// </summary>
        public decimal TotalMarginBalance { get; set; }

        /// <summary>
        /// Total open order initial margin
        /// </summary>
        public decimal TotalOpenOrderInitialMargin { get; set; }

        /// <summary>
        /// Total positional initial margin
        /// </summary>
        public decimal TotalPositionInitialMargin { get; set; }

        /// <summary>
        /// Total unrealized profit
        /// </summary>
        public decimal TotalUnrealizedProfit { get; set; }

        /// <summary>
        /// Total wallet balance
        /// </summary>
        public decimal TotalWalletBalance { get; set; }

        /// <summary>
        /// Total crossed wallet balance
        /// </summary>
        public decimal TotalCrossWalletBalance { get; set; }

        /// <summary>
        /// Unrealized profit of crossed positions
        /// </summary>
        public decimal TotalCrossUnPnl { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// The time of account info was updated
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
    }
}
