namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Or Disable BNB Burn Spot And Margin Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageChangeBnbBurnSpotAndMarginResult
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>spotBNBBurn</c>"] Is Spot BNB Burn
        /// </summary>
        [JsonPropertyName("spotBNBBurn")]
        public bool IsSpotBnbBurn { get; set; }
    }
}