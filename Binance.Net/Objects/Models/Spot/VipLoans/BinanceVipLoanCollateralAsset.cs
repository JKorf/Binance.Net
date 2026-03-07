namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan collateral asset
    /// </summary>
    public record BinanceVipLoanCollateralAsset
    {
        /// <summary>
        /// ["<c>collateralCoin</c>"] The collateral asset.
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>_1stCollateralRatio</c>"] First collateral ratio
        /// </summary>
        [JsonPropertyName("_1stCollateralRatio")]
        public string FirstCollateralRatio { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>_1stCollateralRange</c>"] First collateral range
        /// </summary>
        [JsonPropertyName("_1stCollateralRange")]
        public string FirstCollateralRange { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>_2ndCollateralRatio</c>"] Second collateral ratio
        /// </summary>
        [JsonPropertyName("_2ndCollateralRatio")]
        public string SecondCollateralRatio { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>_2ndCollateralRange</c>"] Second collateral range
        /// </summary>
        [JsonPropertyName("_2ndCollateralRange")]
        public string SecondCollateralRange { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>_3rdCollateralRatio</c>"] Third collateral ratio
        /// </summary>
        [JsonPropertyName("_3rdCollateralRatio")]
        public string ThirdCollateralRatio { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>_3rdCollateralRange</c>"] Third collateral range
        /// </summary>
        [JsonPropertyName("_3rdCollateralRange")]
        public string ThirdCollateralRange { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>_4thCollateralRatio</c>"] Fourth collateral ratio
        /// </summary>
        [JsonPropertyName("_4thCollateralRatio")]
        public string FourthCollateralRatio { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>_4thCollateralRange</c>"] Fourth collateral ratio range.
        /// </summary>
        [JsonPropertyName("_4thCollateralRange")]
        public string FourthCollateralRange { get; set; } = string.Empty;
    }
}