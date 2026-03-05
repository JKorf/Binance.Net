namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub accounts margin summary
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountsMarginSummary
    {
        /// <summary>
        /// The total asset value in BTC.
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total liability
        /// </summary>
        [JsonPropertyName("totalLiabilityOfBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// The total net asset value in BTC.
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }
        /// <summary>
        /// Sub account details
        /// </summary>
        [JsonPropertyName("subAccountList")]
        public BinanceSubAccountMarginInfo[] SubAccounts { get; set; } = Array.Empty<BinanceSubAccountMarginInfo>();
    }

    /// <summary>
    /// Sub account margin info
    /// </summary>
    public record BinanceSubAccountMarginInfo
    {
        /// <summary>
        /// The sub account email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// The total asset value in BTC.
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total liability
        /// </summary>
        [JsonPropertyName("totalLiabilityOfBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// The total net asset value in BTC.
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }
    }
}
