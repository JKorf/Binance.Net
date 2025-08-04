namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account futures details
    /// </summary>
    public record BinanceSubAccountFuturesDetailsV2
    {
        /// <summary>
        /// Futures account response (USDT margined)
        /// </summary>
        [JsonPropertyName("futureAccountResp")]
        public BinanceSubAccountFuturesDetailV2Usdt UsdtMarginedFutures { get; set; } = default!;

        /// <summary>
        /// Delivery account response (COIN margined)
        /// </summary>
        [JsonPropertyName("deliveryAccountResp")]
        public BinanceSubAccountFuturesDetailV2 CoinMarginedFutures { get; set; } = default!;
    }

    /// <summary>
    /// Sub account futures details
    /// </summary>
    public record BinanceSubAccountFuturesDetailV2
    {
        /// <summary>
        /// Email of the sub account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// List of asset details
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceSubAccountFuturesAsset[] Assets { get; set; } = Array.Empty<BinanceSubAccountFuturesAsset>();
        /// <summary>
        /// Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Fee tier
        /// </summary>
        [JsonPropertyName("feeTier")]
        public int FeeTier { get; set; }
        /// <summary>
        /// Time of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Sub account futures details
    /// </summary>
    public record BinanceSubAccountFuturesDetailV2Usdt : BinanceSubAccountFuturesDetailV2
    {
        /// <summary>
        /// Max quantity which can be withdrawn
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Total initial margin
        /// </summary>
        [JsonPropertyName("totalInitialMargin")]
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// Total maintenance margin
        /// </summary>
        [JsonPropertyName("totalMaintenanceMargin")]
        public decimal TotalMaintenanceMargin { get; set; }
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
        /// Total position initial margin
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
    }
}
