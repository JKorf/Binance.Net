namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub accounts btc value summary
    /// </summary>
    public record BinanceSubAccountSpotAssetsSummary
    {
        /// <summary>
        /// Total records
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Master account total asset value
        /// </summary>
        public decimal MasterAccountTotalAsset { get; set; }
        /// <summary>
        /// Sub account values
        /// </summary>
        [JsonPropertyName("spotSubUserAssetBtcVoList")]
        public IEnumerable<BinanceSubAccountBtcValue> SubAccountsBtcValues { get; set; } = Array.Empty<BinanceSubAccountBtcValue>();
    }

    /// <summary>
    /// Sub account btc value
    /// </summary>
    public record BinanceSubAccountBtcValue
    {
        /// <summary>
        /// Sub account email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Sub account total asset 
        /// </summary>
        public decimal TotalAsset { get; set; }
    }
}
