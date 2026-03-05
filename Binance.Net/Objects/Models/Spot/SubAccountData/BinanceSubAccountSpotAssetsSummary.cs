namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub accounts btc value summary
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountSpotAssetsSummary
    {
        /// <summary>
        /// The total number of returned records.
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }
        /// <summary>
        /// Master account total asset value
        /// </summary>
        [JsonPropertyName("masterAccountTotalAsset")]
        public decimal MasterAccountTotalAsset { get; set; }
        /// <summary>
        /// Spot asset values per sub account.
        /// </summary>
        [JsonPropertyName("spotSubUserAssetBtcVoList")]
        public BinanceSubAccountBtcValue[] SubAccountsBtcValues { get; set; } = Array.Empty<BinanceSubAccountBtcValue>();
    }

    /// <summary>
    /// Sub account btc value
    /// </summary>
    public record BinanceSubAccountBtcValue
    {
        /// <summary>
        /// The sub account email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// The total asset value of the sub account.
        /// </summary>
        [JsonPropertyName("totalAsset")]
        public decimal TotalAsset { get; set; }
    }
}
