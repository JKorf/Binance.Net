namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Or Disable BNB Burn Spot And Margin Result
    /// </summary>
    public record BinanceBrokerageChangeBnbBurnSpotAndMarginResult
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
    }
}