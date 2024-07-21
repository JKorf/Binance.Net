namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Or Disable BNB Burn Margin Interest Result
    /// </summary>
    public record BinanceBrokerageChangeBnbBurnMarginInterestResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Is Interest BNB Burn
        /// </summary> 
        [JsonPropertyName("interestBNBBurn")]
        public bool IsInterestBnbBurn { get; set; }
    }
}