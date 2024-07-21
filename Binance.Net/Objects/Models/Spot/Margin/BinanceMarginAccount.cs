namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Information about margin account
    /// </summary>
    public record BinanceMarginAccount
    {
        /// <summary>
        /// Boolean indicating if this account can borrow
        /// </summary>
        [JsonPropertyName("borrowEnabled")]
        public bool BorrowEnabled { get; set; }
        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        [JsonPropertyName("tradeEnabled")]
        public bool TradeEnabled { get; set; }
        /// <summary>
        /// Boolean indicating if this account can transfer
        /// </summary>
        [JsonPropertyName("transferEnabled")]
        public bool TransferEnabled { get; set; }
        /// <summary>
        /// Collateral margin level
        /// </summary>
        [JsonPropertyName("collateralMarginLevel")]
        public decimal? CollateralMarginLevel { get; set; }
        /// <summary>
        /// Total collateral value in USDT
        /// </summary>
        [JsonPropertyName("totalCollateralValueInUSDT")]
        public decimal? TotalCollateralValueInUSDT { get; set; }
        /// <summary>
        /// Aggregate level of margin
        /// </summary>
        [JsonPropertyName("marginLevel")]
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// Aggregate total balance as BTC
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Aggregate total liability balance of BTC
        /// </summary>
        [JsonPropertyName("totalLiabilityOfBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Aggregate total available net balance of BTC
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public string AccountType { get; set; } = string.Empty;
        /// <summary>
        /// Balance list
        /// </summary>
        [JsonPropertyName("userAssets")]
        public IEnumerable<BinanceMarginBalance> Balances { get; set; } = Array.Empty<BinanceMarginBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public record BinanceMarginBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity that was borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// Fee to need pay by borrowed
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// The quantity that is currently locked in a trade
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// The quantity that is netAsset
        /// </summary>
        [JsonPropertyName("netAsset")]
        public decimal NetAsset { get; set; }
        /// <summary>
        /// The total balance of this asset (Available + Locked)
        /// </summary>
        public decimal Total => Available + Locked;
    }
}
