namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub accounts margin summary
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountsMarginSummary
    {
        /// <summary>
        /// ["<c>totalAssetOfBtc</c>"] The total asset value in BTC.
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalLiabilityOfBtc</c>"] Total liability
        /// </summary>
        [JsonPropertyName("totalLiabilityOfBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalNetAssetOfBtc</c>"] The total net asset value in BTC.
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }
        /// <summary>
        /// ["<c>subAccountList</c>"] Sub account details
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
        /// ["<c>email</c>"] The sub account email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalAssetOfBtc</c>"] The total asset value in BTC.
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalLiabilityOfBtc</c>"] Total liability
        /// </summary>
        [JsonPropertyName("totalLiabilityOfBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalNetAssetOfBtc</c>"] The total net asset value in BTC.
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }
    }
}

