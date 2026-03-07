namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// BNB Burn Status
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageBnbBurnStatus
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

        /// <summary>
        /// ["<c>interestBNBBurn</c>"] Is Interest BNB Burn
        /// </summary>
        [JsonPropertyName("interestBNBBurn")]
        public bool IsInterestBnbBurn { get; set; }
    }
}