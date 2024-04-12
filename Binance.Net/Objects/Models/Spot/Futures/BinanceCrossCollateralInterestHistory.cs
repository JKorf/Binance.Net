namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Interest history
    /// </summary>
    public class BinanceCrossCollateralInterestHistory
    {
        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Interest asset
        /// </summary>
        [JsonProperty("interestCoin")]
        public string InterestAsset { get; set; } = string.Empty;
        /// <summary>
        /// Interest
        /// </summary>
        [JsonProperty("interest")]
        public decimal Interest { get; set; }

        /// <summary>
        /// Interest free limit used
        /// </summary>
        [JsonProperty("interestFreeLimitUsed")]
        public decimal InterestFreeLimitUsed { get; set; }
        /// <summary>
        /// Principal interest
        /// </summary>
        [JsonProperty("principalForInterest")]
        public decimal PrincipalInterest { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [JsonProperty("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
