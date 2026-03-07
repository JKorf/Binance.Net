namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Permissions of the current API key
    /// </summary>
    [SerializationModel]
    public record BinanceAPIKeyPermissions
    {
        /// <summary>
        /// ["<c>ipRestrict</c>"] Whether the key is restricted to specific IP addresses.
        /// </summary>
        [JsonPropertyName("ipRestrict")]
        public bool IpRestrict { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Creation time of the key
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>enableWithdrawals</c>"] This option allows you to withdraw via API. You must apply the IP Access Restriction filter in order to enable withdrawals
        /// </summary>
        [JsonPropertyName("enableWithdrawals")]
        public bool EnableWithdrawals { get; set; }
        /// <summary>
        /// ["<c>permitsUniversalTransfer</c>"] This option authorizes this key to transfer funds between your master account and your sub account instantly
        /// </summary>
        [JsonPropertyName("permitsUniversalTransfer")]
        public bool PermitsUniversalTransfer { get; set; }
        /// <summary>
        /// ["<c>enableInternalTransfer</c>"] Authorizes this key to be used for a dedicated universal transfer API to transfer multiple supported currencies. Each business's own transfer API rights are not affected by this authorization
        /// </summary>
        [JsonPropertyName("enableInternalTransfer")]
        public bool EnableInternalTransfer { get; set; }
        /// <summary>
        /// ["<c>enableVanillaOptions</c>"] Authorizes this key to Vanilla options trading
        /// </summary>
        [JsonPropertyName("enableVanillaOptions")]
        public bool EnableVanillaOptions { get; set; }
        /// <summary>
        /// ["<c>enableReading</c>"] Authorizes the reading of account info
        /// </summary>
        [JsonPropertyName("enableReading")]
        public bool EnableReading { get; set; }
        /// <summary>
        /// ["<c>enableFutures</c>"] Authorizes futures trading. API Key created before your futures account opened does not support futures API service
        /// </summary>
        [JsonPropertyName("enableFutures")]
        public bool EnableFutures { get; set; }
        /// <summary>
        /// ["<c>enableMargin</c>"] Authorizes margin. This option can be adjusted after the Cross Margin account transfer is completed
        /// </summary>
        [JsonPropertyName("enableMargin")]
        public bool EnableMargin { get; set; }
        /// <summary>
        /// ["<c>enableSpotAndMarginTrading</c>"] Spot and margin trading allowed
        /// </summary>
        [JsonPropertyName("enableSpotAndMarginTrading")]
        public bool EnableSpotAndMarginTrading { get; set; }
        /// <summary>
        /// ["<c>enablePortfolioMarginTrading</c>"] Portfolio margin trading enabled
        /// </summary>
        [JsonPropertyName("enablePortfolioMarginTrading")]
        public bool EnablePortfolioMarginTrading { get; set; }
        /// <summary>
        /// ["<c>tradingAuthorityExpirationTime</c>"] Expiration time for spot and margin trading permission
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("tradingAuthorityExpirationTime")]
        public DateTime? TradingAuthorityExpirationTime { get; set; }
        /// <summary>
        /// ["<c>enableFixApiTrade</c>"] Authorizes FIX API trading.
        /// </summary>
        [JsonPropertyName("enableFixApiTrade")]
        public bool EnableFixApiTrade { get; set; }
        /// <summary>
        /// ["<c>enableFixReadOnly</c>"] Authorizes FIX data reading.
        /// </summary>
        [JsonPropertyName("enableFixReadOnly")]
        public bool EnableFixReadOnly { get; set; }
    }
}

