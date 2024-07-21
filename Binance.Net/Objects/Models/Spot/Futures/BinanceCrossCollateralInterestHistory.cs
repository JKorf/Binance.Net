namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Interest history
    /// </summary>
    public record BinanceCrossCollateralInterestHistory
    {
        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Interest asset
        /// </summary>
        [JsonPropertyName("interestCoin")]
        public string InterestAsset { get; set; } = string.Empty;
        /// <summary>
        /// Interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }

        /// <summary>
        /// Interest free limit used
        /// </summary>
        [JsonPropertyName("interestFreeLimitUsed")]
        public decimal InterestFreeLimitUsed { get; set; }
        /// <summary>
        /// Principal interest
        /// </summary>
        [JsonPropertyName("principalForInterest")]
        public decimal PrincipalInterest { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
