namespace Binance.Net.Objects.Models.Spot
{
    internal record BinanceTravelRuleAddressWrapper
    {
        [JsonPropertyName("addressVerifyItemList")]
        public BinanceTravelRuleAddress[] Addresses { get; set; } = [];
    }

    /// <summary>
    /// Travel rule address
    /// </summary>
    public record BinanceTravelRuleAddress
    {
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Token
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Wallet address
        /// </summary>
        [JsonPropertyName("walletAddress")]
        public string WalletAddress { get; set; } = string.Empty;
        /// <summary>
        /// Address questionnaire answers
        /// </summary>
        [JsonPropertyName("addressQuestionnaire")]
        public Dictionary<string, object> AddressQuestionnaire { get; set; } = new();
    }
}
