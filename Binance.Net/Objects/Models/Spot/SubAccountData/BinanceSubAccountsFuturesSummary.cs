namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub accounts futures summary
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountsFuturesSummary
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset these totals are reported in.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
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

        /// <summary>
        /// ["<c>subAccountList</c>"] Futures summary information per sub account.
        /// </summary>
        [JsonPropertyName("subAccountList")]
        public BinanceSubAccountFuturesInfo[] SubAccounts { get; set; } = Array.Empty<BinanceSubAccountFuturesInfo>();
    }

    /// <summary>
    /// Sub account future details
    /// </summary>
    public record BinanceSubAccountFuturesInfo
    {
        /// <summary>
        /// ["<c>email</c>"] The sub account email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
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
        /// <summary>
        /// ["<c>asset</c>"] The asset these totals are reported in.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
    }
}

