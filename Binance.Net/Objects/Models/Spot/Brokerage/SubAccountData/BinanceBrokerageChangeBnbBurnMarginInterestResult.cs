namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Or Disable BNB Burn Margin Interest Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageChangeBnbBurnMarginInterestResult
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>interestBNBBurn</c>"] Is Interest BNB Burn
        /// </summary> 
        [JsonPropertyName("interestBNBBurn")]
        public bool IsInterestBnbBurn { get; set; }
    }
}