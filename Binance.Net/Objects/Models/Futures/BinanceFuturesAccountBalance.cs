namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Information about an account
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesAccountBalance
    {
        /// <summary>
        /// Account alias
        /// </summary>
        [JsonPropertyName("accountAlias")]
        public string AccountAlias { get; set; } = string.Empty;

        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// The total balance of this asset
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal WalletBalance { get; set; }

        /// <summary>
        /// Crossed wallet balance
        /// </summary>
        [JsonPropertyName("crossWalletBalance")]
        public decimal CrossWalletBalance { get; set; }

        /// <summary>
        /// Unrealized profit of crossed positions
        /// </summary>
        [JsonPropertyName("crossUnPnl")]
        public decimal? CrossUnrealizedPnl { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Usd futures account balance
    /// </summary>
    [SerializationModel]
    public record BinanceUsdFuturesAccountBalance : BinanceFuturesAccountBalance
    {
        /// <summary>
        /// Maximum quantity for transfer out
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }

        /// <summary>
        /// Whether the asset can be used as margin in Multi-Assets mode
        /// </summary>
        [JsonPropertyName("marginAvailable")]
        public bool? MarginAvailable { get; set; }
    }

    /// <summary>
    /// Coin futures account balance
    /// </summary>
    [SerializationModel]
    public record BinanceCoinFuturesAccountBalance : BinanceFuturesAccountBalance
    {
        /// <summary>
        /// Available for withdraw
        /// </summary>
        [JsonPropertyName("withdrawAvailable")]
        public decimal WithdrawAvailable { get; set; }
    }
}
