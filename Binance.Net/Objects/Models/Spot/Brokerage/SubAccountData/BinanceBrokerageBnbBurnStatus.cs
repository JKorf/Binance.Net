namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// BNB Burn Status
    /// </summary>
    public record BinanceBrokerageBnbBurnStatus
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Is Spot BNB Burn
        /// </summary>
        [JsonPropertyName("spotBNBBurn")]
        public bool IsSpotBnbBurn { get; set; }
        
        /// <summary>
        /// Is Interest BNB Burn
        /// </summary>
        [JsonPropertyName("interestBNBBurn")]
        public bool IsInterestBnbBurn { get; set; }
    }
}