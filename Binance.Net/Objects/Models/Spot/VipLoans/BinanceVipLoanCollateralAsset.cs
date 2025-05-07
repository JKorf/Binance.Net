namespace Binance.Net.Objects.Models.Spot.VipLoans
{
    /// <summary>
    /// VIP Loan collateral asset
    /// </summary>
    public record BinanceVipLoanCollateralAsset
    {
        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// First collateral ratio
        /// </summary>
        [JsonPropertyName("_1stCollateralRatio")]
        public string FirstCollateralRatio { get; set; } = string.Empty;
        /// <summary>
        /// First collateral range
        /// </summary>
        [JsonPropertyName("_1stCollateralRange")]
        public string FirstCollateralRange { get; set; } = string.Empty;
        /// <summary>
        /// Second collateral ratio
        /// </summary>
        [JsonPropertyName("_2ndCollateralRatio")]
        public string SecondCollateralRatio { get; set; } = string.Empty;
        /// <summary>
        /// Second collateral range
        /// </summary>
        [JsonPropertyName("_2ndCollateralRange")]
        public string SecondCollateralRange { get; set; } = string.Empty;
        /// <summary>
        /// Third collateral ratio
        /// </summary>
        [JsonPropertyName("_3rdCollateralRatio")]
        public string ThirdCollateralRatio { get; set; } = string.Empty;
        /// <summary>
        /// Third collateral range
        /// </summary>
        [JsonPropertyName("_3rdCollateralRange")]
        public string ThirdCollateralRange { get; set; } = string.Empty;
        /// <summary>
        /// Fourth collateral ratio
        /// </summary>
        [JsonPropertyName("_4thCollateralRatio")]
        public string FourthCollateralRatio { get; set; } = string.Empty;
        /// <summary>
        /// Fourth collateral range
        /// </summary>
        [JsonPropertyName("_4thCollateralRange")]
        public string FourthCollateralRange { get; set; } = string.Empty;
    }
}