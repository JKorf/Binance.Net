namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account futures details
    /// </summary>
    public record BinanceSubAccountFuturesDetailsV2
    {
        /// <summary>
        /// ["<c>futureAccountResp</c>"] Futures account response (USDT margined)
        /// </summary>
        [JsonPropertyName("futureAccountResp")]
        public BinanceSubAccountFuturesDetailV2Usdt UsdtMarginedFutures { get; set; } = default!;

        /// <summary>
        /// ["<c>deliveryAccountResp</c>"] Delivery account response (COIN margined)
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
        /// ["<c>email</c>"] Email of the sub account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>assets</c>"] List of asset details
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceSubAccountFuturesAsset[] Assets { get; set; } = Array.Empty<BinanceSubAccountFuturesAsset>();
        /// <summary>
        /// ["<c>canDeposit</c>"] Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// ["<c>canTrade</c>"] Can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// ["<c>canWithdraw</c>"] Can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// ["<c>feeTier</c>"] Fee tier
        /// </summary>
        [JsonPropertyName("feeTier")]
        public int FeeTier { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Time of the data
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
        /// ["<c>maxWithdrawAmount</c>"] Max quantity which can be withdrawn
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>totalInitialMargin</c>"] Total initial margin
        /// </summary>
        [JsonPropertyName("totalInitialMargin")]
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// ["<c>totalMaintenanceMargin</c>"] Total maintenance margin
        /// </summary>
        [JsonPropertyName("totalMaintenanceMargin")]
        public decimal TotalMaintenanceMargin { get; set; }
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
        /// ["<c>totalPositionInitialMargin</c>"] Total position initial margin
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
    }
}

